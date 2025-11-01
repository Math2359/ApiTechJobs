using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request;

public class NovoUsuarioRequest
{
    public string Senha { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public EnumPerfil Perfil { get; set; }
    public string Documento { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
}
