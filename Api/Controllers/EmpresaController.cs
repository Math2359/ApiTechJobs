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
    public class EmpresaController(IEmpresaService empresaService) : ControllerBase
    {
        /// <summary>
        /// Obtém os dados da empresa logada
        /// </summary>
        /// <returns>Dados da empresa</returns>
        [AutorizarPerfis(EnumPerfil.Empresa)]
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

        [AutorizarPerfis(EnumPerfil.Empresa)]
        [HttpPost("aplicacao-vaga/{idAplicacao}/{situacao}")]
        public IActionResult RetornarResultado([FromRoute] int idAplicacao, [FromRoute] EnumSituacao situacao)
        {
            empresaService.RetornarResultado(idAplicacao, situacao);

            return NoContent();
        }

        [AutorizarPerfis(EnumPerfil.Empresa)]
        [HttpGet("aplicacao-vaga/{idAplicacao}")]
        public async Task<IActionResult> ObterDadosAplicacaoCandidato([FromRoute] int idAplicacao)
        {
            var aplicacao = await empresaService.ObterDadosAplicacaoCandidato(idAplicacao);

            if (aplicacao == null)
                return NotFound();

            return Ok(aplicacao);
        }

        [AutorizarPerfis(EnumPerfil.Empresa)]
        [HttpGet("informacoes")]
        public IActionResult ObterInformacoesPorUsuario()
        {
            var informacoes = empresaService.ObterInformacoesPorUsuario(User.ObterId());

            return Ok(informacoes);
        }

        [AutorizarPerfis(EnumPerfil.Empresa, EnumPerfil.Candidato)]
        [HttpGet("informacoes/{idEmpresa}")]
        public IActionResult ObterInformacoesPorId([FromRoute] int idEmpresa)
        {
            var informacoes = empresaService.ObterInformacoesPorId(idEmpresa);

            if (informacoes == null)
                return NotFound();

            return Ok(informacoes);
        }

        [AutorizarPerfis(EnumPerfil.Empresa)]
        [HttpPut("informacoes")]
        public IActionResult AtualizarInformacoes([FromBody] AtualizarInformacoesEmpresaRequest request)
        {
            empresaService.AtualizarInformacoesEmpresa(User.ObterId(), request);

            return NoContent();
        }
    }
}
