using Model.Attributes;

namespace Model;

public class AgendamentoEntrevista
{
    [IgnorarInsert]
    public int Id { get; set; }
    public int IdAplicacao { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan Hora { get; set; }
    public string Local { get; set; } = string.Empty;
    public string? Observacao { get; set; }
}
