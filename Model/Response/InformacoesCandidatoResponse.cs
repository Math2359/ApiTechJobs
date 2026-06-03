using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class InformacoesCandidatoResponse(InformacaoCandidato? informacaoCandidato, DadosVagasCandidatoDTO? dadosVagas, IEnumerable<ExperienciaCandidatoDTO> experiencias)
    {
        public int VagasAplicadas { get; set; } = dadosVagas?.VagasAplicadas ?? 0;
        public int ProcessosAtivos { get; set; } = dadosVagas?.ProcessosAtivos ?? 0;
        public string? Descricao { get; set; } = informacaoCandidato?.Descricao;
        public string? Habilidades { get; set; } = informacaoCandidato?.Habilidades;
        public string? EmailPessoal { get; set; } = informacaoCandidato?.EmailPessoal;
        public string? EmailCorporativo { get; set; } = informacaoCandidato?.EmailCorporativo;
        public string? Telefone { get; set; } = informacaoCandidato?.Telefone;
        public string? Linkedin { get; set; } = informacaoCandidato?.Linkedin;
        public string? Github { get; set; } = informacaoCandidato?.Github;
        public string? Preferencias { get; set; } = informacaoCandidato?.Preferencias;
        public string? Cidade { get; set; } = informacaoCandidato?.Cidade;
        public string? Estado { get; set; } = informacaoCandidato?.Estado;
        public int? AnosExperiencia { get; set; } = informacaoCandidato?.AnosExperiencia;
        public string? Area { get; set; } = informacaoCandidato?.Area;
        public IEnumerable<ExperienciaCandidatoDTO> Experiencias { get; set; } = experiencias;
    }
}
