using Model.Attributes;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ExperienciaCandidato
    {
        public int IdCandidato { get; set; }
        public EnumTipoExperiencia TipoExperiencia { get; set; }
        public string Instituicao { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
