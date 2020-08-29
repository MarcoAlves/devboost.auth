using devboost.jwt.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace devboost.jwt
{
    public static class TokenService
    {

        public static string GenerateToken(User user)
        {

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Secret));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expiration = DateTime.UtcNow.AddHours(2);

            //var token = new JwtSecurityToken(
            //    issuer: null,
            //    audience: null,
            //    expires: expiration,
            //    signingCredentials: creds
            //);

            //return new JwtSecurityTokenHandler().WriteToken(token);


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return token;

        }

    }
}
