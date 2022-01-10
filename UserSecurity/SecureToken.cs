using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ForumAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace ForumAPI.UserSecurity
{
    public class SecureToken
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecureToken(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateToken(User user)
        {
            // Declare claims
            // A claim is a statement about a subject by an issuer. Claims represent attributes of the subject that are useful in the context of authentication and authorization operations.
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Generate a security key with the string in our appsettings as a parameter
            // Symmetric key is a string which is used to encrypt the data and with the same string, we can decrypt the data.
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("TokenSettings:SigningKey").Value));

            // Sign credentials
            // Represents the cryptographic key and security algorithms that are used to generate a digital signature.
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);



            // Placeholder for all the attributes related to the issued token.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Gets or sets the output claims to be included in the issued token.
                Expires = System.DateTime.Now.AddDays(1), // Expire token after 1 day.
                SigningCredentials = credentials // Gets or sets the credentials that are used to sign the token.
            };

            // Create a SecurityTokenHandler designed for creating and validating Json Web Tokens.
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor); // Creates a Json Web Token (JWT).



            return tokenHandler.WriteToken(token); // Serializes a JwtSecurityToken into a JWT in Compact Serialization Format.
        }

        public int GetUserId() {
            try {
                checked {
                    var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    return userId;
                }
            }
            catch
            {
                return -1;
            }
        } 
    }
}