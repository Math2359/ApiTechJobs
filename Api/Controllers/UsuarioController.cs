using Api.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Enum;
using Model.Request;
using Services.Interfaces;

namespace Api.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        [HttpPost]
        public IActionResult NovoUsuario([FromBody] NovoUsuarioRequest request)
        {
            _usuarioService.NovoUsuario(request);

            return Ok();
        }

        [HttpPost("token")]
        public IActionResult LogarUsuario([FromBody] LogarUsuarioRequest request)
        {
            try
            {
                var token = _usuarioService.LogarUsuario(request);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
