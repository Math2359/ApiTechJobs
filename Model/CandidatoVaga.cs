using Model.Enum;
using Utils;

namespace Model;

public class CandidatoVaga
{

    [IgnoreInsert]
    public int Id { get; set; }
    public int IdCandidato { get; set; }
    public int IdVaga { get; set; }
    public EnumSituacao Situacao { get; set; }
}
