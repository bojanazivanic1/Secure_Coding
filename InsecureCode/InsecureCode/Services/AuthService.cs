using AutoMapper;
using InsecureCode.DTO;
using InsecureCode.Exceptions;
using InsecureCode.Interfaces.IRepository;
using InsecureCode.Interfaces.IServices;
using InsecureCode.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InsecureCode.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            if ((await _unitOfWork.Users.Get(x => x.Email == registerUserDto.Email)) != null)
                throw new BadRequestException("That username is already in use!");

            User user = _mapper.Map<User>(registerUserDto);

            user.Password = CreatePasswordHash(registerUserDto.Password);

            await _unitOfWork.Users.Add(user);
            await _unitOfWork.Save();
        }

        public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
        {
            User user = await _unitOfWork.Users.Get(x => x.Email == loginUserDto.Email) ?? 
                throw new NotFoundException("User not found.");

            if (!VerifyPasswordHash(loginUserDto.Password, user.Password))
            {
                throw new NotFoundException("Wrong password.");
            }

            return CreateToken(user);
        }
        public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            User user = await _unitOfWork.Users.Get(x => x.Email == resetPasswordDto.Email) ??
                throw new NotFoundException("User not found.");

            user.Password = CreatePasswordHash(resetPasswordDto.Password);

            await _unitOfWork.Users.Save();
        }

        private string CreatePasswordHash(string password)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }

        private bool VerifyPasswordHash(string dtoPassword, string userPassword)
        {
            return CreatePasswordHash(dtoPassword) == userPassword;
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
                expires: DateTime.MaxValue,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}