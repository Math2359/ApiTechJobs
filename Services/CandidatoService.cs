using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Model;
using Model.Enum;
using Model.Request;
using Model.Response;
using Repositories;
using Repositories.Generico.Interface;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class CandidatoService(CandidatoRepository candidatoRepository, CandidatoVagaRepository candidatoVagaRepository, VagaRepository vagaRepository) : ICandidatoService
{
    private readonly CandidatoRepository _candidatoRepository = candidatoRepository;
    private readonly CandidatoVagaRepository _candidatoVagaRepository = candidatoVagaRepository;
    private readonly VagaRepository _vagaRepository = vagaRepository;

    private readonly string _bucketName = "s3-bucket-techjobs";
    private readonly string _folder = "cv";

    public void Adicionar(Candidato candidato) => _candidatoRepository.Adicionar(candidato);

    private async Task<string> UploadFileAsync(IFormFile file, string folder)
    {
        var s3Client = new AmazonS3Client();

        if (file == null || file.Length == 0)
            throw new Exception("Arquivo inválido.");

        // Nome final no bucket
        var fileKey = $"{folder}/{Guid.NewGuid()}_{file.FileName}";

        // Faz o upload
        using (var newMemoryStream = new MemoryStream())
        {
            await file.CopyToAsync(newMemoryStream);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = fileKey,
                BucketName = _bucketName,
                ContentType = file.ContentType
            };

            var transferUtility = new TransferUtility(s3Client);

            await transferUtility.UploadAsync(uploadRequest);
        }

        return fileKey;
    }

    public async Task AplicarVaga(AplicarVagaRequest aplicarVaga)
    {
        var candidato = _candidatoRepository.ObterCandidatoPorIdUsuario(aplicarVaga.IdUsuario);

        string fileKey = await UploadFileAsync(aplicarVaga.IFile, _folder);

        _candidatoVagaRepository.Adicionar(new CandidatoVaga
        {
            FileKey = fileKey,
            IdCandidato = candidato.Id,
            IdVaga = aplicarVaga.IdVaga,
            Situacao = EnumSituacao.EmAnalise
        });
    }

    public IList<AplicacaoCandidatoResponse> ObterAplicacoes(int idUsuario) =>_candidatoVagaRepository.ObterAplicacoesPorCandidato(idUsuario);

    public DashboardCandidatoResponse ObterDadosDashboard(int idUsuario)
    {
        int vagas = _vagaRepository.ObterVagasDisponiveis();

        var dados = _candidatoVagaRepository.ObterDadosDashboard(idUsuario);
        dados.VagasDisponiveis = vagas;

        return dados;
    }
}
