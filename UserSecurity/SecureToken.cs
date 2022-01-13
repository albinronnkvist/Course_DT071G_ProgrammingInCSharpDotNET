using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ForumAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace ForumAPI.UserSecurity
{
    public class SecureToken
    {
        // Inject IConfiguration into the constructor to create an instance of the configuration class which is used to access configuration file elements.
        // Also inject IHttpContextAccessor to access the HttpContext-object which is used to access information in requests and responsens.
        // Store these instances in private fields to be able to access them inside the class.
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecureToken(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }



        // Create a JSON Web Token.
        // Pass a User object in the method.
        public string CreateToken(User user)
        {
            // Declare claims which are pieces of information about the user.
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Generate a symmetric key with the hidden value that is stored inside the user-secret file.
            // We access the hidden value with the configuration object.
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("TokenSettings:SigningKey").Value));

            // Sign credentials
            // Represents the cryptographic key and security algorithms that are used to generate a digital signature.
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Placeholder for all the attributes related to the issued token.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Set the output claims to be included in the issued token.
                Expires = System.DateTime.Now.AddDays(1), // Expire token after 1 day.
                SigningCredentials = credentials // Set the credentials that are used to sign the token.
            };

            // Create an instance of the SecurityTokenHandler class which is designed for creating Json Web Tokens.
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor); // Create the Json Web Token (JWT).

            return tokenHandler.WriteToken(token); // Serialize the JwtSecurityToken into a JWT in Compact Serialization Format.
        }



        // Get a user id by reading the NameIdentifier claim inside a JWT which is stored inside the HttpContext object.
        // If the claim can be converted to an integer, then we return the value. Otherwise we return "-1", which is an id no user can have.
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