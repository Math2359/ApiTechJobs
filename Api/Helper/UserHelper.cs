using System.Security.Claims;

namespace Api.Helper;

public static class UserHelper
{
    /// <summary>
    /// Obtém o identificador numérico do usuário armazenado no <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="claims">O <see cref="ClaimsPrincipal"/> que contém as claims do usuário.</param>
    /// <returns>O valor da claim "Id" convertido para <see cref="int"/>.</returns>
    /// <remarks>
    /// Este método busca a claim com o tipo "Id" (string) e a converte para <see cref="int"/>.
    /// Se <paramref name="claims"/> for nulo, se a claim "Id" estiver ausente ou se o valor da claim
    /// não for um inteiro válido, ocorrerão exceções em tempo de execução.
    /// </remarks>
    /// <example>
    /// // Exemplo de uso:
    /// // int usuarioId = User.ObterId();
    /// </example>
    public static int ObterId(this ClaimsPrincipal claims) => int.Parse(claims.FindFirstValue("Id") ?? "");
}
