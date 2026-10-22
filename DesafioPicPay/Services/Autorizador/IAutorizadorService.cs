namespace DesafioPicPay.Services.Autorizador;

public interface IAutorizadorService
{
    public Task<bool> AutorizadorAsync();
}