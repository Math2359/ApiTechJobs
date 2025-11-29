using Model;
using Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces;

public interface ICandidatoService
{
    void Adicionar(Candidato candidato);
    Task AplicarVaga(AplicarVagaRequest aplicarVaga);
}
