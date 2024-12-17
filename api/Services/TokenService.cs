using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Data.Models;
using api.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenService :ItokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
          private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration config)
        {
            
            _config = config; 
            var signingKey = _config["JWT:SigningKey"];
            if (string.IsNullOrWhiteSpace(signingKey))
          {
             throw new ArgumentNullException("JWT:SigningKey", "La clave JWT no puede ser nula o vacía.");
          }

          _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }

       public string CreateToken(AppUser user)
       {
         var claims = new List<Claim>
          {
          //  new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "defaultEmail@example.com"),
           new Claim(JwtRegisteredClaimNames.GivenName,  user.UserName ?? ""),
           new Claim("userId", user.Id.ToString()), // Aquí se agrega el UserId como un claim adicional
            // new Claim(ClaimTypes.Role, GetUserRolesAsync(user).Result) // Usa GetUserRolesAsync
          };

          // Usar HmacSha512Signature para la firma
           var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

           var tokenDescriptor = new SecurityTokenDescriptor
          {
               Subject = new ClaimsIdentity(claims),
               Expires = DateTime.Now.AddDays(7),
               Issuer = _config["JWT:Issuer"],
               Audience = _config["JWT:Audience"],
               SigningCredentials = creds // Asegúrate de agregar esta línea
          };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
          return tokenHandler.WriteToken(token); // Esto debería generar un token firmado
        }

    }
}