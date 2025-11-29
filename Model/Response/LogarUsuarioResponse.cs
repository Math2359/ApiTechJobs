using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response;

public class LogarUsuarioResponse
{
    public string Token { get; set; } = string.Empty;
    public string NomeUsuario { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public EnumPerfil Perfil { get; set; }
}
