using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request;

public class ObterTodasVagasRequest
{
    public decimal? SalarioInicio { get; set; }
    public decimal? SalarioFim { get; set; }
    public string? TermoBusca { get; set; }
}
