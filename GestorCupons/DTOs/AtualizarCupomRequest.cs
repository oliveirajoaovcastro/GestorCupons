namespace GestorCupons.DTOs;

public class AtualizarCupomRequest
{
    public string Codigo { get; set; } = string.Empty;
    public int IdAgencia { get; set; }
    public int PercentualDesconto { get; set; }
    public DateTime DataInicioVigencia { get; set; }
    public DateTime? DataFimVigencia { get; set; }
}