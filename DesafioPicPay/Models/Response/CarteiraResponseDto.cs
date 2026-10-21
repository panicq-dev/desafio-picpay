namespace DesafioPicPay.Models.Response;

public class CarteiraResponseDto
{
    public string NomeCompleto { get; set; }
    public string CPFCNPJ { get; set; }
    public string Email { get; set; }
    public decimal SaldoConta { get; set; }
    public TipoEntidade TipoEntidade { get; set; }
}