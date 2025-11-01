using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Repositories.Generico;
using Repositories.Generico.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class CandidatoRepository(IConfiguration configuration) : GenericRepository<Candidato>(configuration)
{

}
