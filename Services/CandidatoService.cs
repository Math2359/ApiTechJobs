using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Model;
using Model.DTO;
using Model.Enum;
using Model.Request;
using Model.Response;
using Repositories;
using Services.Interfaces;
using Services.Utils.Interface;

namespace Services;

public class CandidatoService(CandidatoRepository _candidatoRepository, InformacaoCandidatoRepository _informacaoCandidatoRepository,
    ExperienciaCandidatoRepository _experienciaCandidatoRepository, CandidatoVagaRepository _candidatoVagaRepository, IAwsService _awsService) : ICandidatoService
{
    private readonly string _folder = "cv";

    public void Adicionar(Candidato candidato) => _candidatoRepository.Adicionar(candidato);


    public async Task AplicarVaga(AplicarVagaRequest aplicarVaga)
    {
        var candidato = _candidatoRepository.ObterCandidatoPorIdUsuario(aplicarVaga.IdUsuario);

        string fileKey = await _awsService.UploadFileAsync(aplicarVaga.IFile, _folder);

        _candidatoVagaRepository.Adicionar(new CandidatoVaga
        {
            FileKey = fileKey,
            IdCandidato = candidato.Id,
            IdVaga = aplicarVaga.IdVaga,
            Situacao = EnumSituacao.EmAnalise
        });
    }

    public IList<AplicacaoCandidatoResponse> ObterAplicacoes(int idUsuario) => _candidatoVagaRepository.ObterAplicacoesPorCandidato(idUsuario);

    public InformacoesCandidatoResponse ObterInformacoesPorUsuario(int idUsuario)
    {
        var informacoes = _informacaoCandidatoRepository.ObterInformacoesPorUsuario(idUsuario);
        var dadosVagas = _candidatoVagaRepository.ObterDadosDashboard(idUsuario);
        var experiencias = _experienciaCandidatoRepository.ObterExperienciasCandidato(idUsuario);

        return new(informacoes, dadosVagas, experiencias);
    }

    public void AtualizarInformacoesCandidato(int idUsuario, AtualizarInformacoesCandidatoRequest request)
    {
        var candidato = _candidatoRepository.ObterCandidatoPorIdUsuario(idUsuario);

        var informacao = _informacaoCandidatoRepository.ObterInformacoesPorUsuario(idUsuario);

        if (informacao != null)
        {
            informacao.Descricao = request.Descricao;
            informacao.Habilidades = request.Habilidades;
            informacao.EmailPessoal = request.EmailPessoal;
            informacao.EmailCorporativo = request.EmailCorporativo;
            informacao.Telefone = request.Telefone;
            informacao.Linkedin = request.Linkedin;
            informacao.Github = request.Github;
            informacao.Preferencias = request.Preferencias;
            informacao.Cidade = request.Cidade;
            informacao.Estado = request.Estado;
            informacao.AnosExperiencia = request.AnosExperiencia;
            informacao.Area = request.Area;

            _informacaoCandidatoRepository.Editar(informacao);
        }
        else
        {
            _informacaoCandidatoRepository.Adicionar(new InformacaoCandidato
            {
                IdCandidato = candidato.Id,
                Descricao = request.Descricao,
                Habilidades = request.Habilidades,
                EmailPessoal = request.EmailPessoal,
                EmailCorporativo = request.EmailCorporativo,
                Telefone = request.Telefone,
                Linkedin = request.Linkedin,
                Github = request.Github
            });
        }


        if (request.Experiencias?.Count > 0)
        {
            _experienciaCandidatoRepository.Excluir(idUsuario);

            foreach (var exp in request.Experiencias)
            {
                _experienciaCandidatoRepository.Adicionar(new ExperienciaCandidato
                {
                    IdCandidato = candidato.Id,
                    TipoExperiencia = exp.TipoExperiencia,
                    Instituicao = exp.Instituicao,
                    Descricao = exp.Descricao,
                    DataInicio = exp.DataInicio,
                    DataFim = exp.DataFim
                });
            }
        }
    }
}
