using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;

namespace DesafioPicPay.Services.Carteiras;

public interface ICarteiraService
{
    public Task<Result<CarteiraEntidade>> ExecutarAsync(string cpfcnpj, string email);
}