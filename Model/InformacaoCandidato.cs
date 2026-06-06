using Model.Attributes;
using Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class InformacaoCandidato
    {
        [IgnorarInsert]
        public int Id { get; set; }
        public int IdCandidato { get; set; }
        public string? Descricao { get; set; }
        public string? Habilidades { get; set; }
        public string? EmailPessoal { get; set; }
        public string? EmailCorporativo { get; set; }
        public string? Telefone { get; set; }
        public string? Linkedin { get; set; }
        public string? Github { get; set; }
        public string? Preferencias { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public int? AnosExperiencia { get; set; }
        public string? Area { get; set; }

        public void AtualizarModel(AtualizarInformacoesCandidatoRequest request)
        {
            Descricao = request.Descricao ?? Descricao;
            Habilidades = request.Habilidades ?? Habilidades;
            EmailPessoal = request.EmailPessoal ?? EmailPessoal;
            EmailCorporativo = request.EmailCorporativo ?? EmailCorporativo;
            Telefone = request.Telefone ?? Telefone;
            Linkedin = request.Linkedin ?? Linkedin;
            Github = request.Github ?? Github;
            Preferencias = request.Preferencias ?? Preferencias;
            Cidade = request.Cidade ?? Cidade;
            Estado = request.Estado ?? Estado;
            AnosExperiencia = request.AnosExperiencia ?? AnosExperiencia;
            Area = request.Area ?? Area;
        }
    }
}
