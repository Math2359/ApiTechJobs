using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response;

public class DashboardEmpresaResponse
{
    public int VagasDisponiveis { get; set; }
    public int Candidatos { get; set; }
    public int Aprovacoes { get; set; }
}
