using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class AtualizarInformacoesCandidatoRequest
    {
        public string? Descricao { get; set; }
        public string? Habilidades { get; set; }
        public string? EmailPessoal { get; set; }
        public string? EmailCorporativo { get; set; }
        public string? Telefone { get; set; }
        public string? Linkedin { get; set; }
        public string? Github { get; set; }
        public string? Preferencias { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public int? AnosExperiencia { get; set; }
        public string? Area { get; set; }
        public IList<ExperienciaCandidatoDTO>? Experiencias { get; set; }
    }
}
