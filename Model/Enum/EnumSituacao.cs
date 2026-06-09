using System.ComponentModel;

namespace Model.Enum;

public enum EnumSituacao
{
    [Description("Em análise")]
    EmAnalise = 1,
    [Description("Aprovado")]
    Aprovado = 2,
    [Description("Entrevista marcada")]
    Entrevista = 3,
    [Description("Reprovado")]
    Reprovado = 4
}
