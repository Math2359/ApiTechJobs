using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request;

public class ObterTodasVagasRequest
{
    public string? Cargo { get; set; }
    public string? NivelExperiencia { get; set; }
    public string? Modelo { get; set; }
    public string? CEP { get; set; }
}
