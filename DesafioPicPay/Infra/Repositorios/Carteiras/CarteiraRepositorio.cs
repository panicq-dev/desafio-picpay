using AutoMapper;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace DesafioPicPay.Infra.Repositorios.Carteiras;

public class CarteiraRepositorio : ICarteiraRepositorio
{
    private readonly AplicacaoDbContext _contexto;
    private readonly IMapper _mapper;

    public CarteiraRepositorio(AplicacaoDbContext contexto, IMapper mapper)
    {
        _contexto = contexto;
        _mapper = mapper;
    }

    public async Task AddAsync(CarteiraEntidade carteira)
    {
        await _contexto.Carteiras.AddAsync(carteira);
    }

    public Task UpdateAsync(CarteiraEntidade carteira)
    {
        _contexto.Carteiras.Update(carteira);
        return Task.CompletedTask;
    }

    public async Task<List<CarteiraEntidade>> BuscarTodasAsync(CancellationToken cancellationToken)
    {
        return await _contexto.Carteiras
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<CarteiraEntidade?> BuscarCpfCnpjEmail(string cpfcnpj, string email)
    {
        return await _contexto.Carteiras.FirstOrDefaultAsync(c
            => c.CPFCNPJ.Equals(cpfcnpj) || c.Email.Equals(email));
    }

    public async Task<CarteiraEntidade?> BuscarPorId(int idCarteira)
    {
        return await _contexto.Carteiras.FirstOrDefaultAsync(c => c.Id == idCarteira);
    }

    public async Task CommitAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}