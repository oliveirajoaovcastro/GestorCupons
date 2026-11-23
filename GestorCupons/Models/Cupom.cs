namespace GestorCupons.Models;

public class Cupom
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public int IdAgencia { get; set; }
    public int PercentualDesconto { get; set; }
    public DateTime DataInicioVigencia { get; set; }
    public DateTime? DataFimVigencia { get; set; }
    public bool UtilizadoEmApolice { get; set; }
    public bool VinculadoALinkAtivo { get; set; }
    public string CriadoPor { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public string? ModificadoPor { get; set; }
    public DateTime? DataModificacao { get; set; }
}