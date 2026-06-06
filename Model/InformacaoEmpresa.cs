using Model.Attributes;
using Model.Request;
using System;

namespace Model
{
    public class InformacaoEmpresa
    {
        [IgnorarInsert]
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string? Descricao { get; set; }
        public string? Setor { get; set; }
        public string? Tecnologias { get; set; }
        public string? LinkSite { get; set; }

        public void AtualizarModel(AtualizarInformacoesEmpresaRequest request)
        {
            Descricao = request.Descricao ?? Descricao;
            Setor = request.Setor ?? Setor;
            Tecnologias = request.Tecnologias ?? Tecnologias;
            LinkSite = request.LinkSite ?? LinkSite;
        }
    }
}