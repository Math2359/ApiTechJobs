using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class InformacaoCandidato
    {

        [IgnorarInsert]
        public int Id { get; set; }
        public int IdCandidato { get; set; }
        public string? Descricao { get; set; }
        public string? Habilidades { get; set; }
        public string? EmailPessoal { get; set; }
        public string? EmailCorporativo { get; set; }
        public string? Telefone { get; set; }
        public string? Linkedin { get; set; }
        public string? Github { get; set; }
    }
}
