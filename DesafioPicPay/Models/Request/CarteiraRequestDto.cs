using System.ComponentModel.DataAnnotations;

namespace DesafioPicPay.Models.Request;

public class CarteiraRequestDto
{
    [Required(ErrorMessage = "Informe o seu nome completo.")]
    public string NomeCompleto { get; set; }
    [Required(ErrorMessage = "É essencial CPF ou CNPJ")]
    public string CPFCNPJ { get; set; }
    [Required(ErrorMessage = "Informe seu melhor e-mail.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Informe uma senha!")]
    public string Senha { get; set; }
    public TipoEntidade TipoEntidade { get; set; }
}