using Model;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class EmpresaService(EmpresaRepository empresaRepository) : IEmpresaService
{
    private readonly EmpresaRepository _empresaRepository = empresaRepository;

    public int Adicionar(Empresa empresa) => _empresaRepository.Adicionar(empresa);
}
