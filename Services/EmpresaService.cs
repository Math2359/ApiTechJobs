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

public class EmpresaService(EmpresaRepository empresaRepository, VagaRepository vagaRepository) : IEmpresaService
{
    private readonly EmpresaRepository _empresaRepository = empresaRepository;
    private readonly VagaRepository _vagaRepository = vagaRepository;

    public int Adicionar(Empresa empresa) => _empresaRepository.Adicionar(empresa);

    public Empresa ObterEmpresaPorIdUsuario(int idUsuario) => _empresaRepository.ObterEmpresaPorIdUsuario(idUsuario);

    public int AdicionarVaga(int idUsuario, AdicionarVagaRequest novaVaga)
    {
        var empresa = _empresaRepository.ObterEmpresaPorIdUsuario(idUsuario);

        var vaga = new Vaga
        {
            Cargo = novaVaga.Cargo,
            Cep = novaVaga.Cep,
            DataCadastro = DateTime.Now,
            DataFimInscricoes = novaVaga.DataFimInscricoes ?? DateTime.Now,
            Descricao = novaVaga.Descricao,
            IdEmpresa = empresa.Id,
            Interna = novaVaga.Interna,
            Modelo = novaVaga.Modelo,
            NivelExperiencia = novaVaga.NivelExperiencia,
            Nome = novaVaga.Nome,
            Numero = novaVaga.Numero,
            SalarioPrevisto = novaVaga.SalarioPrevisto
        };

        return _vagaRepository.Adicionar(vaga);
    }

    public IList<Vaga> ObterVagas(int idUsuario) => _vagaRepository.ObterVagasPorIdUsuarioEmpresa(idUsuario);
}
