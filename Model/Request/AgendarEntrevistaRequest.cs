namespace Model.Request;

public class AgendarEntrevistaRequest
{
    public DateTime Data { get; set; }
    public TimeSpan Hora { get; set; }
    public string Local { get; set; } = string.Empty;
    public string? Observacao { get; set; }
}
