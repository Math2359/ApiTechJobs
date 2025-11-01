using Microsoft.AspNetCore.Authorization;
using Model.Enum;

namespace Api.Configuration;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AutorizarPerfis : AuthorizeAttribute
{
    public AutorizarPerfis(params EnumPerfil[] roles)
    {
        Roles = string.Join(",", roles);
    }
}

