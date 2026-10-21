using DesafioPicPay.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioPicPay.Infra.Repositorios.Carteiras;

public class CarteiraRepositorio : ICarteiraRepositorio
{
    private readonly AplicacaoDbContext _contexto;

    public CarteiraRepositorio(AplicacaoDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task AddAsync(CarteiraEntidade carteira)
    {
        await _contexto.Carteiras.AddAsync(carteira);
    }

    public async Task UpdateAsync(CarteiraEntidade carteira)
    {
        _contexto.Carteiras.Update(carteira);
    }

    public async Task<CarteiraEntidade> BuscarCpfCnpjEmail(string cpfcnpj, string email)
    {
        return await _contexto.Carteiras.FirstOrDefaultAsync(c
            => c.CPFCNPJ.Equals(cpfcnpj) || c.Email.Equals(email));
    }

    public async Task<CarteiraEntidade> BuscarPorId(int idCarteira)
    {
        return await _contexto.Carteiras.FirstOrDefaultAsync(c => c.Id == idCarteira);
    }

    public async Task CommitAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}