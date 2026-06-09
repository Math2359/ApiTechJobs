using Model.Attributes;
using Model.Enum;

namespace Model;

public class NotificacaoUsuario
{
    [IgnorarInsert]
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public EnumTipoNotificacao Tipo { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
    public bool Lida { get; set; }
    public string? PropsAdicionais { get; set; }
}
