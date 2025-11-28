using Api.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Interfaces;
using System.Text.Json;
using Utils;

namespace Api.Configuration;

public class VagaActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            int idUsuario = context.HttpContext.User.ObterId();
            int idVaga = int.Parse(context.ActionArguments.FirstOrDefault(value => value.Key == "id").Value?.ToString() ?? throw new Exception("Id não encontrado."));

            var vagaService = (IVagaService)context.HttpContext.RequestServices.GetService(typeof(IVagaService))!;

            if (!vagaService.ValidarVagaEmpresa(idVaga, idUsuario))
                throw new UnauthorizedAccessException("A vaga não pertence à empresa do usuário autenticado.");

            return;
        }
        catch
        {
            context.Result = new ForbidResult();
        }
    }
}
