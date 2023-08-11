using AutoMapper;
using InsecureCode.DTO;
using InsecureCode.Interfaces.IProviders;
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
        private static IUserDbProvider? _userDbProvider;
        private readonly IConfiguration? _configuration;
        private readonly IMapper? _mapper;

        public AuthService(IUserDbProvider userDbProvider, IConfiguration? configuration, IMapper? mapper)
        {
            _userDbProvider = userDbProvider;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(registerUserDto.Email);
            if (user != null)
            {
                throw new Exception("That username is already in use!");
            }                

            user = _mapper.Map<User>(registerUserDto);

            user.Password = CreatePasswordHash(registerUserDto.Password);

            return await _userDbProvider.AddUserAsync(user);
        }

        public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(loginUserDto.Email) ?? 
                throw new Exception("User not found.");

            if (!VerifyPasswordHash(loginUserDto.Password, user.Password))
            {
                throw new Exception("Wrong password.");
            }

            return CreateToken(user);
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            User? user = await _userDbProvider.FindUserByEmailAsync(resetPasswordDto.Email) ??
                throw new Exception("User not found.");

            user.Password = CreatePasswordHash(resetPasswordDto.Password);

            return await _userDbProvider.SaveChanges(); ;
        }

        private string? CreatePasswordHash(string? password)
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
