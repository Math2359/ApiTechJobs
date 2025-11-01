using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Model;

public class Empresa
{

    [IgnoreInsert]
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
}
