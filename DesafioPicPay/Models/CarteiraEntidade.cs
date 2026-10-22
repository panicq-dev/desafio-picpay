
namespace DesafioPicPay.Models;


public class CarteiraEntidade
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; }
    public string CPFCNPJ { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public decimal SaldoConta { get; set; }
    public TipoEntidade TipoEntidade { get; set; }

    public CarteiraEntidade() { }
    public CarteiraEntidade(string nomeCompleto, string cpfcnpj, string email, string senha, TipoEntidade tipoEntidade, decimal saldoConta = 2000)
    {
        NomeCompleto = nomeCompleto;
        CPFCNPJ = cpfcnpj;
        Email = email;
        Senha = senha;
        TipoEntidade = tipoEntidade;
        SaldoConta = saldoConta;
    }

    public void DebitarSaldo(decimal valor)
    {
        SaldoConta -= valor;
    }

    public void CreditarSaldo(decimal valor)
    {
        SaldoConta += valor;
    }
}