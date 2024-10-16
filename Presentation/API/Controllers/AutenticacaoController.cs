using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _autenticacaoService;

        public AutenticacaoController(AutenticacaoService autenticacaoService)
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
            // Implementar lógica de registro
            return Ok();
        }
    }

    // DTOs
    public class LoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class RegistroDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
