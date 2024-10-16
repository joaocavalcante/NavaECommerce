using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Enums;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Services
{
    public class AutenticacaoService : IAutenticacaoService
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

        public async Task RegistrarAsync(RegistroDto dto)
        {
            // Validação básica
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new DomainException("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new DomainException("O email é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Senha))
                throw new DomainException("A senha é obrigatória.");

            // Verificar se o email já está em uso
            var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(dto.Email);
            if (usuarioExistente != null)
                throw new DomainException("O email já está em uso.");

            // Hash da senha
            string senhaHash = HashSenha(dto.Senha);

            // Criar nova entidade Usuario
            var novoUsuario = new Usuario(dto.Nome, dto.Email, senhaHash, Role.Administrador);

            // Adicionar ao repositório
            await _usuarioRepository.AdicionarAsync(novoUsuario);
        }

        private bool VerificarSenha(string senha, string senhaHash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
        }

        private string HashSenha(string senha)
        {
            // Gera um hash seguro para a senha usando BCrypt
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        private string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, usuario.Email),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, usuario.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtLifespan),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
