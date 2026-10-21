using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Infra.Repositorios.Transferencias;
using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;

namespace DesafioPicPay.Services.Transferencias;

public class TransferenciaService : ITransferenciaService
{

    public readonly CarteiraRepositorio _carteiraRepositorio;
    private readonly TransferenciaRepositorio _transferenciaRepositorio;

    public TransferenciaService(CarteiraRepositorio carteiraRepositorio,
        TransferenciaRepositorio transferenciaRepositorio)
    {
        _carteiraRepositorio = carteiraRepositorio;
        _transferenciaRepositorio = transferenciaRepositorio;
    }

    public async Task<Result<TransferenciaResponseDto>> ExecutarAsync(TransferenciaRequest transferencia)
    {
        var pagador = await _carteiraRepositorio.BuscarPorId(transferencia.PagadorId);
        var recebedor = await _carteiraRepositorio.BuscarPorId(transferencia.RecebedorId);

        if (recebedor is null || pagador is null)
            return Result<TransferenciaResponseDto>.Falha("Nenhuma entidade foi encontrada.");
        if (pagador.TipoEntidade == TipoEntidade.Lojista)
            return Result<TransferenciaResponseDto>.Falha("Lojista não pode efetuar transação.");
        if (pagador.SaldoConta < transferencia.Valor)
            return Result<TransferenciaResponseDto>.Falha("Saldo insuficiente.");

        pagador.DebitarSaldo(transferencia.Valor);
        recebedor.CreditarSaldo(transferencia.Valor);

        var transacao = new TransferenciaEntidade(pagador.Id, recebedor.Id, transferencia.Valor);
        using (var transferenciaEscopo = await _transferenciaRepositorio.BeginTransactionAsync())
        {
            try
            {
                var atualizarTarefas = new List<Task>
                {
                    _carteiraRepositorio.UpdateAsync(pagador),
                    _carteiraRepositorio.UpdateAsync(recebedor),
                    _transferenciaRepositorio.AddTransacao(transacao)
                };

                await Task.WhenAll(atualizarTarefas);

                await _carteiraRepositorio.CommitAsync();
                await _transferenciaRepositorio.CommitAsync();
                await transferenciaEscopo.CommitAsync();
            }
            catch (Exception e)
            {
                await transferenciaEscopo.RollbackAsync();
                return Result<TransferenciaResponseDto>.Falha("Falha ao realizar a transferência");

            }
        }

        var response = new TransferenciaResponseDto(transacao.IdTransferencia, pagador.Id, recebedor.Id, transferencia.Valor);
        return Result<TransferenciaResponseDto>.Sucesso(response, "Transferencia realizada com sucesso.");

    }

}