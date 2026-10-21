using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;

namespace DesafioPicPay.Services.Transferencias;

public interface ITransferenciaService
{
    public Task<Result<TransferenciaResponseDto>>ExecutarAsync(TransferenciaRequestDto transferencia);
}