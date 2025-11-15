using Model;
using Model.Request;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class VagaService(VagaRepository vagaRepository) : IVagaService
{
    private readonly VagaRepository _vagaRepository = vagaRepository;

    public Vaga? ObterVaga(int id) => _vagaRepository.ObterPorId(id);

    public void Excluir(int id) => _vagaRepository.Excluir(id);

    public IList<Vaga> ObterTodas(ObterTodasVagasRequest request) => _vagaRepository.ObterTodos(request);
}
