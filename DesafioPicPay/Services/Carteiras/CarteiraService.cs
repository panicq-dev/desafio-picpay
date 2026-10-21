using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;

namespace DesafioPicPay.Services.Carteiras;

public class CarteiraService : ICarteiraService
{
    public readonly CarteiraRepositorio _repositorio;

    public CarteiraService(CarteiraRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<Result<CarteiraEntidade>> ExecutarAsync(string cpfcnpj, string email)
    {
        var usuario = await _repositorio.BuscarCpfCnpjEmail(cpfcnpj, email);
        if (usuario is null)
        {
            return Result<CarteiraEntidade>.Falha("Carteira não encontrada. Verifique o CPF, CNPJ ou e-mail.");
        }
        return Result<CarteiraEntidade>.Sucesso(usuario, "Sucesso: carteira encontrada.");

    }
}