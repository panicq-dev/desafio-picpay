using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;

namespace DesafioPicPay.Services.Notificacoes;

public class NotificacaoService : INotificacaoService
{
    public void EnviarNotificacao()
    {
        Task.Delay(1000);
        Console.WriteLine("Você recebeu uma transferência!");
    }
}