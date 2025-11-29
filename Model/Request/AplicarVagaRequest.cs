using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request;

public class AplicarVagaRequest
{
    public IFormFile IFile { get; set; }
    public int IdVaga { get; set; }
    public int IdUsuario { get; set; }
}
