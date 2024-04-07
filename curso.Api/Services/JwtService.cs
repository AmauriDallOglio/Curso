using curso.Api.Models.Usuarios;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace curso.Api.Services
{
    public class JwtService : IAuthenticationServices
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(UsuarioViewModelOutPut usuarioViewModelOut)
        {
            var secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwTConfigurations:Secret").Value);
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOut.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOut.Login),
                    new Claim(ClaimTypes.Email, usuarioViewModelOut.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerate = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerate);

            return token;
        }
    }
}
