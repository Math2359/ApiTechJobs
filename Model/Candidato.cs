using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Model;

public class Candidato
{

    [IgnoreInsert]
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public int? IdEmpresa { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}
