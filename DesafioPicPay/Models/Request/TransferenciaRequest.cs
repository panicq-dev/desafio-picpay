using System.ComponentModel.DataAnnotations;

namespace DesafioPicPay.Models.Request;

public class TransferenciaRequest
{
    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O ID do pagador é obrigatório.")]
    public int PagadorId { get; set; } 

    [Required(ErrorMessage = "O ID do recebedor é obrigatório.")]
    public int RecebedorId { get; set; }
}
