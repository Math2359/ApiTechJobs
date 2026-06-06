using Api.Configuration;
using Api.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Enum;
using Model.Request;
using Model.Response;
using Services;
using Services.Interfaces;
using System.Security.Claims;
using Utils;

namespace Api.Controllers
{
    [Route("empresa")]
    [ApiController]
    [ProducesErrorResponseType(typeof(ErroResponse))]
    [AutorizarPerfis(EnumPerfil.Empresa)]
    public class EmpresaController(IEmpresaService empresaService) : ControllerBase
    {
        /// <summary>
        /// Obtém os dados da empresa logada
        /// </summary>
        /// <returns>Dados da empresa</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Empresa))]
        [HttpGet]
        public IActionResult ObterDados()
        {
            try
            {
                var empresa = empresaService.ObterEmpresaPorIdUsuario(User.ObterId());

                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.GerarRespostaErro());
            }
        }

        [HttpPost("aplicacao-vaga/{idAplicacao}/{situacao}")]
        public IActionResult RetornarResultado([FromRoute] int idAplicacao, [FromRoute] EnumSituacao situacao)
        {
            empresaService.RetornarResultado(idAplicacao, situacao);

            return NoContent();
        }

        [HttpGet("informacoes")]
        public IActionResult ObterInformacoesPorUsuario()
        {
            var informacoes = empresaService.ObterInformacoesPorUsuario(User.ObterId());

            return Ok(informacoes);
        }

        [HttpPut("informacoes")]
        public IActionResult AtualizarInformacoes([FromBody] AtualizarInformacoesEmpresaRequest request)
        {
            empresaService.AtualizarInformacoesEmpresa(User.ObterId(), request);

            return NoContent();
        }
    }
}
