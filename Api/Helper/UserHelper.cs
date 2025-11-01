using System.Security.Claims;

namespace Api.Helper;

public static class UserHelper
{
    public static int ObterId(this ClaimsPrincipal claims) => int.Parse(claims.FindFirstValue("Id") ?? "");
}
