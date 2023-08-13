using AutoMapper;
using Google.Authenticator;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureCode.DTO;
using SecureCode.Interfaces.IProviders;
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
        private static IUserDbProvider _userDbProvider;
        private static IEmailService _emailService;
        private static ITotpService _totpService;
        private readonly IConfiguration _configuration;
        private readonly IMapper? _mapper;

        public AuthService(IConfiguration configuration, 
                            IMapper mapper, 
                            IUserDbProvider userDbProvider, 
                            IEmailService emailService,
                            ITotpService totpService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userDbProvider = userDbProvider;
            _emailService = emailService;
            _totpService = totpService;
        }

        public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(registerUserDto.Email);
            if(user != null)
            {
                throw new Exception("That username is already in use!");
            }

            user = _mapper.Map<User>(registerUserDto);

            user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            user.Password = CreatePasswordHash(registerUserDto.Password, user.Salt);
            user.VerificatonCode = Math.Abs(Guid.NewGuid().GetHashCode()).ToString().Substring(0, 6);
            user.TotpSecretKey = GenerateRandomKey();

            await _userDbProvider.AddUserAsync(user);

            await Send2FACodeByEmailAsync(user.Email, user.VerificatonCode);
        }

        public async Task ConfirmEmailAsync(CodeDto codeDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(codeDto.Email) ??
                throw new Exception("User doesn't exist.");

            if (!user.VerificatonCode.Equals(codeDto.Code))
            {
                throw new Exception("Invalid code.");
            }

            user.VerifiedAt = DateTime.Now;

            await _userDbProvider.SaveChanges(); ;
        }
        
        public async Task<TotpSetup> LoginUserAsync(LoginUserDto loginUserDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(loginUserDto.Email) ??
                throw new Exception("User not found.");

            if (!VerifyPasswordHash(loginUserDto.Password, user.Password, user.Salt))
            {
                throw new Exception("Wrong password.");
            }

            if (user.VerifiedAt == null)
            {
                throw new Exception("Not verified!");
            }

            return _totpService.Generate("Sec-login", user.Email, user.TotpSecretKey);
        }
        public async Task<string> LoginConfirmAsync(CodeDto codeDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(codeDto.Email) ??
                throw new Exception("User doesn't exist.");

            if (!_totpService.Validate(user.TotpSecretKey, int.Parse(codeDto.Code)))
            {
                throw new Exception("Code is not valid!");
            }               

            return CreateToken(user);
        }
        public async Task<TotpSetup> ResetPasswordRequestAsync(EmailDto emailDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(emailDto.Email) ??
                throw new Exception("User not found.");

            return _totpService.Generate("Sec-password", user.Email, user.TotpSecretKey);
        }

        public async Task ResetPasswordConfirmAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userDbProvider.FindUserByEmailAsync(resetPasswordDto.Email) ??
                throw new Exception("User doesn't exist.");

            if (!_totpService.Validate(user.TotpSecretKey, int.Parse(resetPasswordDto.Code)))
            {
                throw new Exception("Code is not valid!");
            }

            user.Password = CreatePasswordHash(resetPasswordDto.Password, user.Salt);

            await _userDbProvider.SaveChanges(); ;
        }

        #region sending email for 2fa
        /*
        public async Task<bool> LoginUserAsync(LoginUserDto loginUserDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(loginUserDto.Email) ??
                throw new Exception("User not found.");

            if (!VerifyPasswordHash(loginUserDto.Password, user.Password, user.Salt))
            {
                throw new Exception("Wrong password.");
            }

            if(user.VerifiedAt == null)
            {
                throw new Exception("Not verified!");
            }

            user.LoginCode = CreateRandomCode();
            user.LoginCodeExpires = DateTime.Now.AddMinutes(5);

            await _userDbProvider.SaveChanges();

            return await Send2FACodeByEmailAsync(user.Email, user.LoginCode);
        }
        public async Task<string> LoginConfirmAsync(CodeDto codeDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(codeDto.Email) ??
                throw new Exception("User doesn't exist.");

            if (!user.LoginCode.Equals(codeDto.Code))
            {
                throw new Exception("Invalid code.");
            }

            if (user.LoginCodeExpires < DateTime.Now)
            {
                throw new Exception("Code expired.");
            }

            user.PasswordResetCode = null;
            user.ResetCodeExpires = null;

            await _userDbProvider.SaveChanges();

            return CreateToken(user);
        }
        public async Task<bool> ResetPasswordRequestAsync(EmailDto emailDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(emailDto.Email) ??
                throw new Exception("User not found.");

            user.PasswordResetCode = CreateRandomCode();
            user.ResetCodeExpires = DateTime.Now.AddMinutes(5);

            await _userDbProvider.SaveChanges();

            return await Send2FACodeByEmailAsync(user.Email, user.PasswordResetCode);
        }
        public async Task<bool> ResetPasswordConfirmAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userDbProvider.FindUserByEmailAsync(resetPasswordDto.Email) ??
                throw new Exception("User doesn't exist.");

            if (!user.PasswordResetCode.Equals(resetPasswordDto.Code))
            {
                throw new Exception("Invalid code.");
            }

            if(user.ResetCodeExpires < DateTime.Now)
            {
                throw new Exception("Code expired.");
            }

            user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            user.Password = CreatePasswordHash(resetPasswordDto.Password, user.Salt);
            user.PasswordResetCode = null;
            user.ResetCodeExpires = null;

            return await _userDbProvider.SaveChanges(); ;
        }
        */
        #endregion

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
                new Claim("Email", user.Email),
                new Claim("Id", user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:Token").Value));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Send2FACodeByEmailAsync(string email, string code)
        {
            try
            {
                string subject = "Authentication Code";
                string body = $"Your 2FA code is: {code}";

                await _emailService.SendEmail(email, subject, body);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send 2FA code.\n" + ex.Message);
            }
        }

        private string GenerateRandomKey(int length = 20)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[length];
                rng.GetBytes(bytes);
                var chars = bytes.Select(b => validChars[b % validChars.Length]);
                return new string(chars.ToArray());
            }
        }
    }
}
