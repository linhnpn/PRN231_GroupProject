using GroupProject_HRM_Library.Constaints;
using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Services
{
    public class JWTServices : IJWTServices
    {
        public ObjectToken ExtractToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constains.JWT_KEY);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var username = jwtToken.Claims.First(x => x.Type == "unique_name").Value;
            var role = jwtToken.Claims.First(x => x.Type == "role").Value;
            var employeeID = jwtToken.Claims.First(x => x.Type == "nameid").Value;
            return new ObjectToken { 
                Username = username,    
                Role = role,
                EmployeeID = employeeID,
            };
        }

        public string GenerateJWTToken(int employeeID, string username, string role)
        {
            var key = Encoding.ASCII.GetBytes(Constains.JWT_KEY); 
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, employeeID+""),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Thời gian hết hạn của JWT
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

    }
}
