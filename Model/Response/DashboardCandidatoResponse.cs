using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response;

public class DashboardCandidatoResponse
{
    public int VagasDisponiveis { get; set; }
    public int VagasAplicadas { get; set; }
    public int ProcessosAtivos { get; set; }
}
