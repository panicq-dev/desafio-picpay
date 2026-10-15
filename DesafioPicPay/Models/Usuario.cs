
using System.Runtime.CompilerServices;
using DesafioPicPay.Services;
using Microsoft.EntityFrameworkCore;
namespace DesafioPicPay.Models;

[Index(nameof(Cpf), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class Usuario
{
    private readonly TransacaoService _transacaoService;
    public required string NomeCompleto { get; set; }
    public required string Cpf { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public required TipoUsuario  TipoUsuario { get; set; }
    public double Carteira { get; set; } = 5000;
    

}