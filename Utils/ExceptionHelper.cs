using Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils;

public static class ExceptionHelper
{
    public static ErroResponse GerarRespostaErro(this Exception ex) => new()
    {
        Mensagem = ex.Message
    };
}
