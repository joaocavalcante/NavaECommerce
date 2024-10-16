using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _autenticacaoService.LoginAsync(dto.Email, dto.Senha);
            return Ok(new { Token = token });
        }

        // Possível endpoint para registro
        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDto dto)
        {
            await _autenticacaoService.RegistrarAsync(dto);
            return Ok(new { Mensagem = "Usuário registrado com sucesso." });
        }
    }

    // DTOs
    public class LoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
