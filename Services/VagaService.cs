using Amazon.S3;
using Amazon.S3.Model;
using Model;
using Model.Request;
using Model.Response;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class VagaService(VagaRepository vagaRepository, CandidatoVagaRepository candidatoVagaRepository) : IVagaService
{
    private readonly VagaRepository _vagaRepository = vagaRepository;
    private readonly CandidatoVagaRepository _candidatoVagaRepository = candidatoVagaRepository;
    
    private readonly string _bucketName = "s3-bucket-techjobs";
    private readonly string _folder = "cv";

    public Vaga? ObterVaga(int id) => _vagaRepository.ObterPorId(id);

    public void Excluir(int id) => _vagaRepository.Excluir(id);

    public IList<VagaCandidatoResponse> ObterTodas(ObterTodasVagasRequest request) => _vagaRepository.ObterTodos(request);

    public bool ValidarVagaEmpresa(int idVaga, int idUsuarioEmpresa) => _vagaRepository.ObterVagaPorIdUsuarioEmpresa(idVaga, idUsuarioEmpresa) != null;

    public VagaEmpresaResponse ObterVagaEmpresaPorId(int id)
    {
        var vaga = _vagaRepository.ObterPorId(id);
        var aplicacoes = _candidatoVagaRepository.ObterAplicacoes(id);

        return new VagaEmpresaResponse
        {
            Aplicacoes = aplicacoes ?? [],
            Vaga = vaga
        };
    }

    public string GerarUrlAssinada(int id)
    {
        var aplicacao = _candidatoVagaRepository.ObterPorId(id);

        var s3client = new AmazonS3Client();

        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = aplicacao.FileKey,
            Expires = DateTime.UtcNow.AddMinutes(15),
        };

        // Gera a URL
        return s3client.GetPreSignedURL(request);
    }
}
