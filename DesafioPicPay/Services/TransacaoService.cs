using DesafioPicPay.Models;

namespace DesafioPicPay.Services;

public class TransacaoService
{
    private readonly UsuarioService _usuarioService;
    public TransacaoService(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public void Transferir(Usuario usuario, string cpf, double valor)
    {
        usuario.Carteira -=  valor;
        var remetente = _usuarioService.BuscarPorCpf(cpf);
        remetente.Carteira += valor;
        
    }
}