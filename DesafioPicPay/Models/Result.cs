namespace DesafioPicPay.Infra.Result;

public class Result<T>
{
    public bool EhSucesso { get; set; }
    public T Valor  { get; set; }
    public string Mensagem { get; set; }

    public Result(bool ehSucesso, T valor,  string mensagem)
    {
        EhSucesso = ehSucesso;
        Valor = valor;
        Mensagem = mensagem;
    }

    public static Result<T> Falha(string mensagem)
    {
        return new Result<T>(false, default!, mensagem);
    }

    public static Result<T> Sucesso(T valor, string mensagem)
    {
        return new Result<T>(true, valor, mensagem);
    }
}