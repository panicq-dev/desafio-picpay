using System.ComponentModel.DataAnnotations;

namespace DesafioPicPay.Models.Request;

/// <summary>Dados enviados para criar uma carteira.</summary>
public class CarteiraRequestDto
{
    /// <summary>Nome completo do titular da carteira.</summary>
    [Required(ErrorMessage = "Informe o seu nome completo.")]
    public string NomeCompleto { get; set; }
    /// <summary>CPF ou CNPJ do titular. Deve ser único no sistema.</summary>
    [Required(ErrorMessage = "É essencial CPF ou CNPJ")]
    public string CPFCNPJ { get; set; }
    /// <summary>E-mail do titular. Deve ser único no sistema.</summary>
    [Required(ErrorMessage = "Informe seu melhor e-mail.")]
    public string Email { get; set; }
    /// <summary>Senha associada à carteira.</summary>
    [Required(ErrorMessage = "Informe uma senha!")]
    public string Senha { get; set; }
    /// <summary>Tipo de titular: 0 para usuário ou 1 para lojista.</summary>
    public TipoEntidade TipoEntidade { get; set; }
}