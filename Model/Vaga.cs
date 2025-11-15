using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class Vaga
{

    [IgnorarInsert]
    public int Id { get; set; }
    public int IdEmpresa { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string NivelExperiencia { get; set; } = string.Empty;
    public string? Cep { get; set; }
    public string? Numero { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal? SalarioPrevisto { get; set; }
    public bool Interna { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataFimInscricoes { get; set; }
}