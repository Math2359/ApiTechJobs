using Model;
using Model.Request;
using Model.Response;

namespace Services.Interfaces;

public interface IUsuarioService
{
    void NovoUsuario(NovoUsuarioRequest novoUsuario);
    LogarUsuarioResponse LogarUsuario(LogarUsuarioRequest logarUsuario);
}
