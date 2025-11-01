using Model;
using Model.Request;

namespace Services.Interfaces;

public interface IUsuarioService
{
    void NovoUsuario(NovoUsuarioRequest novoUsuario);
    string LogarUsuario(LogarUsuarioRequest logarUsuario);
}
