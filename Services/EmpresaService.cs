using Model;
using Model.Enum;
using Model.Request;
using Model.Response;
using Repositories;
using Services.Interfaces;

namespace Services;

public class EmpresaService(EmpresaRepository empresaRepository, VagaRepository vagaRepository, CandidatoVagaRepository candidatoVagaRepository) : IEmpresaService
{
    private readonly EmpresaRepository _empresaRepository = empresaRepository;
    private readonly VagaRepository _vagaRepository = vagaRepository;
    private readonly CandidatoVagaRepository _candidatoVagaRepository = candidatoVagaRepository;

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

    public IList<VagaResponse> ObterVagas(int idUsuario) => _vagaRepository.ObterVagasPorIdUsuarioEmpresa(idUsuario);

    public void RetornarResultado(int idAplicacao, EnumSituacao situacao) => _candidatoVagaRepository.Editar(new CandidatoVaga
    {
        Id = idAplicacao,
        Situacao = situacao,
    });

    public DashboardEmpresaResponse ObterDadosDashboard(int idUsuario)
    {
        int vagas = _vagaRepository.ObterVagasDisponiveis(idUsuario);

        var dados = _candidatoVagaRepository.ObterDadosDashboardEmpresa(idUsuario);
        dados.VagasDisponiveis = vagas;

        return dados;
    }
}
