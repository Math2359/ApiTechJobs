using System.ComponentModel;
using System.Reflection;

namespace Services.Utils;

public static class EnumExtensions
{
    public static string ObterDescricao(this Enum valor)
    {
        var membro = valor.GetType().GetMember(valor.ToString()).FirstOrDefault();
        var descricao = membro?.GetCustomAttribute<DescriptionAttribute>();

        return descricao?.Description ?? valor.ToString();
    }
}
