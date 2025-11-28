using Model.Enum;

namespace Model;

public class CredenciaisUsuarioDTO
{
    public int Id { get; set; }
    public EnumPerfil Perfil { get; set; }
    public string Senha { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}
