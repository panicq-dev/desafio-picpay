using DesafioPicPay.Models;

namespace DesafioPicPay.Infra.Repositorios.Carteiras;

public interface ICarteiraRepositorio
{
    Task AddAsync(CarteiraEntidade carteira);
    Task UpdateAsync(CarteiraEntidade carteira);
    Task<CarteiraEntidade> BuscarCpfCnpjEmail(string cpfcnpj, string email);
    Task CommitAsync();
}