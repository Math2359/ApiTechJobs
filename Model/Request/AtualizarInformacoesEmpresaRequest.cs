using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class AtualizarInformacoesEmpresaRequest
    {
        public string? Setor { get; set; }
        public string? Tecnologias { get; set; }
        public string? Descricao { get; set; }
        public string? LinkSite { get; set; }
    }
}
