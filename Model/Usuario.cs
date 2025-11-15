using Model.Attributes;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class Usuario
{
    [IgnorarInsert]
    public int Id { get; set; }
    public EnumPerfil Perfil { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
}
