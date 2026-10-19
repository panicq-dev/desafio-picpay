using DesafioPicPay.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace DesafioPicPay.Infra.Repositorios.Transferencias;

public class TransferenciaRepositorio : ITransferenciaRepositorio
{
    private readonly AplicacaoDbContext _contexto;

    public TransferenciaRepositorio(AplicacaoDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task AddTransacao(TransferenciaEntidade transferencia)
    {
        await _contexto.Transferencias.AddAsync(transferencia);
    }

    public async Task CommitAsync()
    {
        await _contexto.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _contexto.Database.BeginTransactionAsync();
    }
}