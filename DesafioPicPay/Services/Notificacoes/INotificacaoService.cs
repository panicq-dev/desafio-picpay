using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;

namespace DesafioPicPay.Services.Notificacoes;

public interface INotificacaoService
{
    public void EnviarNotificacao();
}