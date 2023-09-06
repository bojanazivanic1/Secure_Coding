using AutoMapper;
using Google.Authenticator;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Security.Application;
using SecureCode.DTO;
using SecureCode.Exceptions;
using SecureCode.Interfaces.IRepository;
using SecureCode.Interfaces.IServices;
using SecureCode.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SecureCode.Services
{
    public class AuthService : IAuthService
    {
        private static IEmailService? _emailService;
        private static ITotpService? _totpService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IConfiguration configuration, 
                            IMapper mapper, 
                            IEmailService emailService,
                            ITotpService totpService,
                            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
            _totpService = totpService;
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            if (registerUserDto.UserRole == EUserRole.ADMIN)
                throw new UnauthorizedException("You cannot register like admin.");

            if ((await _unitOfWork.Users.Get(x => x.Email == registerUserDto.Email)) != null)
                throw new BadRequestException("That email is already in use!");

            User user = _mapper.Map<User>(registerUserDto);

            user.Name = Sanitizer.GetSafeHtmlFragment(user.Name);

            user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            user.Password = CreatePasswordHash(registerUserDto.Password!, user.Salt);
            user.VerificatonCode = Math.Abs(Guid.NewGuid().GetHashCode()).ToString().Substring(0, 6);
            user.TotpSecretKey = GenerateRandomKey();

            await _unitOfWork.Users.Add(user);
            await _unitOfWork.Save();

            await Send2FACodeByEmailAsync(user.Email!, user.VerificatonCode);
        }

        public async Task ConfirmEmailAsync(CodeDto codeDto)
        {
            User user = await _unitOfWork.Users.Get(x => x.Email == codeDto.Email) ??
                throw new BadRequestException("User doesn't exist.");

            if (!user.VerificatonCode.Equals(codeDto.Code))
            {
                throw new BadRequestException("Invalid code.");
            }

            user.VerifiedAt = DateTime.Now;

            await _unitOfWork.Save();
        }
        
        public async Task<TotpSetup> LoginUserAsync(LoginUserDto loginUserDto)
        {
            User user = await _unitOfWork.Users.Get(x => x.Email == loginUserDto.Email) ??
                throw new BadRequestException("User not found.");

            if (!VerifyPasswordHash(loginUserDto.Password!, user.Password!, user.Salt!))
            {
                throw new BadRequestException("Wrong password.");
            }

            if (user.VerifiedAt == default(DateTime))
            {
                throw new UnauthorizedException("Not verified!");
            }

            return _totpService!.Generate("Sec-login", user.Email!, user.TotpSecretKey);
        }
        public async Task<string> LoginConfirmAsync(CodeDto codeDto)
        {
            User user = await _unitOfWork.Users.Get(x => x.Email == codeDto.Email) ??
                throw new BadRequestException("User doesn't exist.");

            if (!_totpService!.Validate(user.TotpSecretKey, int.Parse(codeDto.Code!)))
            {
                throw new BadRequestException("Code is not valid!");
            }               

            return CreateToken(user);
        }
        public async Task<TotpSetup> ResetPasswordRequestAsync(EmailDto emailDto)
        {
            User? user = await _unitOfWork.Users.Get(x => x.Email == emailDto.Email) ??
                throw new BadRequestException("User not found.");

            return _totpService!.Generate("Sec-password", user.Email!, user.TotpSecretKey);
        }

        public async Task ResetPasswordConfirmAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _unitOfWork.Users.Get(x => x.Email == resetPasswordDto.Email) ??
                throw new BadRequestException("User doesn't exist.");

            if (!_totpService!.Validate(user.TotpSecretKey, int.Parse(resetPasswordDto.Code!)))
            {
                throw new BadRequestException("Code is not valid!");
            }

            user.Password = CreatePasswordHash(resetPasswordDto.Password!, user.Salt!);

            await _unitOfWork.Save();
        }

        public string CreatePasswordHash(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool VerifyPasswordHash(string dtoPassword, string userPassword, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(dtoPassword, salt) == userPassword;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email!),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()!),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:Token").Value!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Send2FACodeByEmailAsync(string email, string code)
        {
            try
            {
                string subject = "Authentication Code";
                string body = $"<html><body><h3>Your 2FA code is:</h3><h1>{code}</h1></body></html>";
                
                await _emailService!.SendEmail(email, subject, body);
                return true;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException("Failed to send 2FA code.\n" + ex.Message);
            }
        }

        private string GenerateRandomKey(int length = 20)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[length];
                rng.GetBytes(bytes);
                var chars = bytes.Select(b => validChars[b % validChars.Length]);
                return new string(chars.ToArray());
            }
        }

    }
}