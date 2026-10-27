using System.ComponentModel.DataAnnotations;

namespace DesafioPicPay.Models.Request;

/// <summary>Dados enviados para realizar uma transferência.</summary>
public class TransferenciaRequestDto
{
    /// <summary>Valor a transferir, maior que zero.</summary>
    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    /// <summary>Identificador da carteira de origem. O titular deve ser um usuário.</summary>
    [Required(ErrorMessage = "O ID do pagador é obrigatório.")]
    public int PagadorId { get; set; } 
    /// <summary>Identificador da carteira de destino.</summary>
    [Required(ErrorMessage = "O ID do recebedor é obrigatório.")]
    public int RecebedorId { get; set; }
}
