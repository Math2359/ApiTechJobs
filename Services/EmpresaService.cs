using Model;
using Model.Enum;
using Model.Request;
using Model.Response;
using Repositories;
using Services.Interfaces;
using Services.Utils;
using Services.Utils.Interface;
using System.Text.Json;
using Utils;

namespace Services;

public class EmpresaService(EmpresaRepository empresaRepository, VagaRepository vagaRepository, UsuarioRepository usuarioRepository, CandidatoRepository candidatoRepository, CandidatoVagaRepository candidatoVagaRepository, InformacaoEmpresaRepository informacaoEmpresaRepository, NotificacaoUsuarioRepository notificacaoUsuarioRepository, InformacaoCandidatoRepository informacaoCandidatoRepository, ExperienciaCandidatoRepository experienciaCandidatoRepository, AgendamentoEntrevistaRepository agendamentoEntrevistaRepository, IAwsService awsService) : IEmpresaService
{
    public int Adicionar(Empresa empresa) => empresaRepository.Adicionar(empresa);

    public Empresa ObterEmpresaPorIdUsuario(int idUsuario) => empresaRepository.ObterEmpresaPorIdUsuario(idUsuario);

    public int AdicionarVaga(int idUsuario, AdicionarVagaRequest novaVaga)
    {
        var empresa = empresaRepository.ObterEmpresaPorIdUsuario(idUsuario);

        var vaga = new Vaga
        {
            Cargo = novaVaga.Cargo,
            Cep = novaVaga.Cep,
            DataCadastro = HorarioBrasilia.DataAtual,
            DataFimInscricoes = novaVaga.DataFimInscricoes ?? DateTime.Now,
            Descricao = novaVaga.Descricao,
            IdEmpresa = empresa.Id,
            Interna = novaVaga.Interna,
            Modelo = novaVaga.Modelo,
            NivelExperiencia = novaVaga.NivelExperiencia,
            Nome = novaVaga.Nome,
            Numero = novaVaga.Numero,
            SalarioPrevisto = novaVaga.SalarioPrevisto,
            Tecnologias = novaVaga.Tecnologias,
            Requisitos = novaVaga.Requisitos,
            Beneficios = novaVaga.Beneficios
        };

        return vagaRepository.Adicionar(vaga);
    }

    public IList<VagaResponse> ObterVagas(int idUsuario) => vagaRepository.ObterVagasPorIdUsuarioEmpresa(idUsuario);

    public void RetornarResultado(int idAplicacao, EnumSituacao situacao)
    {
        if (situacao == EnumSituacao.Entrevista)
            throw new Exception("Utilize o endpoint de agendamento para marcar uma entrevista.");

        var aplicacao = candidatoVagaRepository.ObterPorId(idAplicacao)
            ?? throw new Exception("Aplicação não encontrada.");
        var candidato = candidatoRepository.ObterPorId(aplicacao.IdCandidato)
            ?? throw new Exception("Candidato não encontrado.");

        candidatoVagaRepository.Editar(new CandidatoVaga
        {
            Id = idAplicacao,
            Situacao = situacao,
            DataAtualizacao = HorarioBrasilia.DataAtual
        });

        notificacaoUsuarioRepository.Adicionar(new NotificacaoUsuario
        {
            DataCadastro = HorarioBrasilia.DataAtual,
            PropsAdicionais = aplicacao.IdVaga.ToString(),
            IdUsuario = candidato.IdUsuario,
            Lida = false,
            Mensagem = $"A empresa retornou o resultado da sua aplicação: {situacao.ObterDescricao()}",
            Titulo = "Resultado da aplicação!",
            Tipo = EnumTipoNotificacao.RespostaVaga
        });
    }

    public void AgendarEntrevista(int idAplicacao, AgendarEntrevistaRequest request)
    {
        if (request.Data == default)
            throw new Exception("A data da entrevista deve ser informada.");

        if (request.Hora < TimeSpan.Zero || request.Hora >= TimeSpan.FromDays(1))
            throw new Exception("A hora da entrevista é inválida.");

        if (string.IsNullOrWhiteSpace(request.Local))
            throw new Exception("O local da entrevista deve ser informado.");

        if (request.Local.Length > 300)
            throw new Exception("O local da entrevista deve ter no máximo 300 caracteres.");

        if (request.Observacao?.Length > 1000)
            throw new Exception("A observação deve ter no máximo 1000 caracteres.");

        var dataHoraEntrevista = request.Data.Date.Add(request.Hora);

        if (dataHoraEntrevista <= HorarioBrasilia.DataAtual)
            throw new Exception("A entrevista deve ser agendada para uma data e hora futuras.");

        var aplicacao = candidatoVagaRepository.ObterPorId(idAplicacao)
            ?? throw new Exception("Aplicação não encontrada.");
        var candidato = candidatoRepository.ObterPorId(aplicacao.IdCandidato)
            ?? throw new Exception("Candidato não encontrado.");

        var agendamento = new AgendamentoEntrevista
        {
            IdAplicacao = idAplicacao,
            Data = request.Data.Date,
            Hora = request.Hora,
            Local = request.Local,
            Observacao = request.Observacao
        };

        if (!agendamentoEntrevistaRepository.Agendar(agendamento, HorarioBrasilia.DataAtual))
            throw new Exception("Não foi possível agendar a entrevista.");

        notificacaoUsuarioRepository.Adicionar(new NotificacaoUsuario
        {
            DataCadastro = HorarioBrasilia.DataAtual,
            PropsAdicionais = aplicacao.IdVaga.ToString(),
            IdUsuario = candidato.IdUsuario,
            Lida = false,
            Mensagem = $"Sua entrevista foi agendada para {agendamento.Data:dd/MM/yyyy} às {agendamento.Hora:hh\\:mm}, em {agendamento.Local}.",
            Titulo = "Entrevista marcada!",
            Tipo = EnumTipoNotificacao.RespostaVaga
        });
    }

    public async Task<DadosAplicacaoCandidatoResponse?> ObterDadosAplicacaoCandidato(int idAplicacao)
    {
        var aplicacao = candidatoVagaRepository.ObterPorId(idAplicacao);

        if (aplicacao == null)
            return null;

        var informacoes = informacaoCandidatoRepository.ObterInformacoesPorIdCandidato(aplicacao.IdCandidato);
        var experiencias = experienciaCandidatoRepository.ObterExperienciasPorIdCandidato(aplicacao.IdCandidato);
        var urlCv = string.IsNullOrWhiteSpace(aplicacao.FileKey)
            ? null
            : await awsService.PreSignedURL(aplicacao.FileKey);

        return new DadosAplicacaoCandidatoResponse
        {
            InformacaoCandidato = informacoes,
            Experiencias = experiencias,
            Situacao = aplicacao.Situacao,
            DataCadastroAplicacao = aplicacao.DataCadastro,
            UrlCv = urlCv,
            AgendamentoEntrevista = agendamentoEntrevistaRepository.ObterPorIdAplicacao(idAplicacao)
        };
    }

    public InformacoesEmpresaResponse ObterInformacoesPorUsuario(int idUsuario)
    {
        var empresa = empresaRepository.ObterEmpresaPorIdUsuario(idUsuario);

        return ObterInformacoes(empresa);
    }

    public InformacoesEmpresaResponse? ObterInformacoesPorId(int idEmpresa)
    {
        var empresa = empresaRepository.ObterPorId(idEmpresa);

        return empresa == null ? null : ObterInformacoes(empresa);
    }

    private InformacoesEmpresaResponse ObterInformacoes(Empresa empresa)
    {
        var informacoes = informacaoEmpresaRepository.ObterInformacoesPorIdEmpresa(empresa.Id);
        var vagas = vagaRepository.ObterVagasDisponiveisPorIdEmpresa(empresa.Id);
        var dadosVagasEmpresa = candidatoVagaRepository.ObterDadosDashboardEmpresa(empresa.IdUsuario);
        var usuario = usuarioRepository.ObterPorId(empresa.IdUsuario);

        return new(empresa, informacoes, vagas, dadosVagasEmpresa.Candidatos, usuario?.EmailValidado);
    }


    public void AtualizarInformacoesEmpresa(int idUsuario, AtualizarInformacoesEmpresaRequest request)
    {
        var empresa = empresaRepository.ObterEmpresaPorIdUsuario(idUsuario);

        var informacao = informacaoEmpresaRepository.ObterInformacoesPorUsuario(idUsuario);

        if (informacao != null)
        {
            informacao.AtualizarModel(request);

            informacaoEmpresaRepository.Editar(informacao);
        }
        else
        {
            informacaoEmpresaRepository.Adicionar(new InformacaoEmpresa
            {
                IdEmpresa = empresa.Id,
                Descricao = request.Descricao,
                LinkSite = request.LinkSite,
                Setor = request.Setor,
                Tecnologias = request.Tecnologias,
            });
        }
    }

    public async Task<string?> GerarUrlAssinadaFotoPerfil(int idEmpresa)
    {
        var fileKey = empresaRepository.ObterChaveFotoPerfilEmpresa(idEmpresa);

        if (string.IsNullOrWhiteSpace(fileKey))
            return null;

        return await awsService.PreSignedURL(fileKey);
    }
}
