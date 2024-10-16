using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly string _jwtSecret;
        private readonly int _jwtLifespan;

        public AutenticacaoService(IUsuarioRepository usuarioRepository, string jwtSecret, int jwtLifespan)
        {
            _usuarioRepository = usuarioRepository;
            _jwtSecret = jwtSecret;
            _jwtLifespan = jwtLifespan;
        }

        public async Task<string> LoginAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(email);
            if (usuario == null || !VerificarSenha(senha, usuario.SenhaHash))
            {
                throw new DomainException("Credenciais inválidas.");
            }

            return GerarToken(usuario);
        }

        private bool VerificarSenha(string senha, string senhaHash)
        {
            // Implementar verificação de hash (ex: BCrypt)
            // Aqui é simplificado para fins de exemplo
            return senha == senhaHash;
        }

        private string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtLifespan),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
