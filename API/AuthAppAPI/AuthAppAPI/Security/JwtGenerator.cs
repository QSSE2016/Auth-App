

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthAppAPI.Security
{
    public class JwtGenerator
    {
        public string Generate(string email,IConfiguration config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Sectoken = new JwtSecurityToken(config["Jwt:Issuer"],config["Jwt:Issuer"],null,expires: DateTime.Now.AddSeconds(20),signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(Sectoken);
        }
    }
}
