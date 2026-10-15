using DesafioPicPay.Models;

namespace DesafioPicPay.Dto;

public class UsuarioDtoResposta
{
    public string NomeCompleto { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public TipoUsuario TipoUsuario { get; set; }
    public double Carteira { get; set; } 
}