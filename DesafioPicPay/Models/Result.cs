namespace DesafioPicPay.Infra.Result;

/// <summary>Envelope padrão de sucesso ou falha retornado pela API.</summary>
public class Result<T>
{
    /// <summary>Indica se a operação foi concluída com sucesso.</summary>
    public bool EhSucesso { get; set; }
    /// <summary>Dados retornados pela operação; nulo quando ela falha.</summary>
    public T Valor  { get; set; }
    /// <summary>Mensagem de sucesso ou motivo da falha.</summary>
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