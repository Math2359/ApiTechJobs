using Model;
using Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces;

public interface IVagaService
{
    Vaga? ObterVaga(int id);
    void Excluir(int id);
    IList<Vaga> ObterTodas(ObterTodasVagasRequest request);
    bool ValidarVagaEmpresa(int idVaga, int idUsuarioEmpresa);
}
