using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class InformacoesEmpresaResponse(InformacaoEmpresa? informacaoEmpresa, int vagasDisponiveis, int candidatos)
    {
        public string? Setor { get; set; } = informacaoEmpresa?.Setor;
        public string? Tecnologias { get; set; } = informacaoEmpresa?.Tecnologias;
        public string? Descricao { get; set; } = informacaoEmpresa?.Descricao;
        public string? LinkSite { get; set; } = informacaoEmpresa?.LinkSite;
        public int VagasDisponiveis { get; set; } = vagasDisponiveis;
        public int Candidatos { get; set; } = candidatos;
    }
}
