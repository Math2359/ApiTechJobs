using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using Services.Interfaces;
using Utils;

namespace Api.Controllers
{
    [Route("usuario")]
    [ApiController]
    [ProducesErrorResponseType(typeof(ErroResponse))]
    public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="request">Dados para criar usuário</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost]
        public IActionResult NovoUsuario([FromBody] NovoUsuarioRequest request)
        {
            try
            {
                _usuarioService.NovoUsuario(request);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.GerarRespostaErro());
            }
        }

        /// <summary>
        /// Realiza o login de um usuário
        /// </summary>
        /// <param name="request">Dados para login</param>
        /// <returns>O token JWT do usuário</returns>
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.GerarRespostaErro());
            }
        }
    }
}
