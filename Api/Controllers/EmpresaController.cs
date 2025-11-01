using Api.Configuration;
using Api.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Enum;
using Model.Request;
using Services.Interfaces;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("empresa")]
    [ApiController]
    [AutorizarPerfis(EnumPerfil.Empresa)]
    public class EmpresaController(IEmpresaService empresaService) : ControllerBase
    {
        private readonly IEmpresaService _empresaService = empresaService;

        [HttpGet]
        public IActionResult ObterDados()
        {
            var empresa = _empresaService.ObterEmpresaPorIdUsuario(User.ObterId());

            return Ok(empresa);
        }

        [HttpPost("vaga")]
        public IActionResult AdicionarVaga([FromBody] AdicionarVagaRequest request)
        {
            int id = _empresaService.AdicionarVaga(User.ObterId(), request);

            return Ok(id);
        }

        [HttpGet("vaga")]
        public IActionResult ObterVagas()
        {
            var vagas = _empresaService.ObterVagas(User.ObterId());

            return Ok(vagas);
        }
    }
}
