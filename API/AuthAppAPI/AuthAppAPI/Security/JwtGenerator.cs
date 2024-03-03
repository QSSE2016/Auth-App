using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthAppAPI.Security
{
    public class JwtGenerator
    {
        public string Generate(string username)
        {
            return "my_token_lol";
        }
    }
}
