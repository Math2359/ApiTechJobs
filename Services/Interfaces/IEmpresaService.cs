using Model;
using Model.Enum;
using Model.Request;
using Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces;

public interface IEmpresaService
{
    int Adicionar(Empresa empresa);
    Empresa ObterEmpresaPorIdUsuario(int idUsuario);
    int AdicionarVaga(int idUsuario, AdicionarVagaRequest novaVaga);
    IList<VagaResponse> ObterVagas(int idUsuario);
    void RetornarResultado(int idAplicacao, EnumSituacao situacao);
    DashboardEmpresaResponse ObterDadosDashboard(int idUsuario);
}
