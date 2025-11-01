using Model;
using Repositories;
using Repositories.Generico.Interface;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class CandidatoService(CandidatoRepository candidatoRepository) : ICandidatoService
{
    private readonly CandidatoRepository _candidatoRepository = candidatoRepository;

    public void Adicionar(Candidato candidato) => _candidatoRepository.Adicionar(candidato);
}
