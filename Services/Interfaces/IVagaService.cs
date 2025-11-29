using Model;
using Model.Request;
using Model.Response;
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
    IList<VagaCandidatoResponse> ObterTodas(ObterTodasVagasRequest request);
    bool ValidarVagaEmpresa(int idVaga, int idUsuarioEmpresa);
    VagaEmpresaResponse ObterVagaEmpresaPorId(int id);
    string GerarUrlAssinada(int id);
}
