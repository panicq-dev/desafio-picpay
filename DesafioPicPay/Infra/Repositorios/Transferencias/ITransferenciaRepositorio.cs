using DesafioPicPay.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace DesafioPicPay.Infra.Repositorios.Transferencias;

public interface ITransferenciaRepositorio
{
    public Task AddTransacao (TransferenciaEntidade transferencia);
    public Task CommitAsync();
    public Task<IDbContextTransaction> BeginTransactionAsync();
}