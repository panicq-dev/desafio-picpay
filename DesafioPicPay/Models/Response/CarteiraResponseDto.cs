namespace DesafioPicPay.Models.Response;

/// <summary>Dados retornados para uma carteira.</summary>
public class CarteiraResponseDto
{
    /// <summary>Nome completo do titular.</summary>
    public string NomeCompleto { get; set; }
    /// <summary>CPF ou CNPJ do titular.</summary>
    public string CPFCNPJ { get; set; }
    /// <summary>E-mail do titular.</summary>
    public string Email { get; set; }
    /// <summary>Saldo atual da carteira.</summary>
    public decimal SaldoConta { get; set; }
    /// <summary>Tipo de titular: usuário ou lojista.</summary>
    public TipoEntidade TipoEntidade { get; set; }
}