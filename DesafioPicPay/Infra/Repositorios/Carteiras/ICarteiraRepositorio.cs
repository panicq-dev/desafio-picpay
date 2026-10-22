using DesafioPicPay.Models;
using DesafioPicPay.Models.Response;

namespace DesafioPicPay.Infra.Repositorios.Carteiras;

public interface ICarteiraRepositorio
{
    Task AddAsync(CarteiraEntidade carteira);
    Task UpdateAsync(CarteiraEntidade carteira);
    Task<List<CarteiraEntidade>> BuscarTodasAsync(CancellationToken cancellationToken);
    Task<CarteiraEntidade?> BuscarCpfCnpjEmail(string cpfcnpj, string email);
    Task<CarteiraEntidade?> BuscarPorId(int idCarteira);
    Task CommitAsync();
}