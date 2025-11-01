using Model;
using Model.Request;
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
    IList<Vaga> ObterVagas(int idUsuario);
}
