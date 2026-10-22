using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;

namespace DesafioPicPay.Services.Carteiras;

public interface ICarteiraService
{
    public Task<List<CarteiraResponseDto>> BuscarTodasAsync();
    public Task<Result<CarteiraEntidade>> BuscarCarteira(string cpfcnpj, string email);
    public Task<Result<CarteiraResponseDto>> CriarCarteira(CarteiraRequestDto carteiraRequest);
}