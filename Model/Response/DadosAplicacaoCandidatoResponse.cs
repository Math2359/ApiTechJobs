using Model.DTO;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response;

public class DadosAplicacaoCandidatoResponse
{
    public InformacaoCandidato? InformacaoCandidato { get; set; }
    public IEnumerable<ExperienciaCandidatoDTO> Experiencias { get; set; } = [];
    public EnumSituacao Situacao { get; set; }
    public DateTime DataCadastroAplicacao { get; set; }
    public string? UrlCv { get; set; }
}
