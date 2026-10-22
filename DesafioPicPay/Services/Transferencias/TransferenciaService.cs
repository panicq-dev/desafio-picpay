using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Infra.Repositorios.Transferencias;
using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;
using DesafioPicPay.Services.Autorizador;

namespace DesafioPicPay.Services.Transferencias;

public class TransferenciaService : ITransferenciaService
{

    private readonly ICarteiraRepositorio _carteiraRepositorio;
    private readonly ITransferenciaRepositorio _transferenciaRepositorio;

    private readonly AutorizadorService _autorizadorService;
    public TransferenciaService(ICarteiraRepositorio carteiraRepositorio,
        ITransferenciaRepositorio transferenciaRepositorio, AutorizadorService autorizadorService)
    {
        _autorizadorService = autorizadorService;
        _carteiraRepositorio = carteiraRepositorio;
        _transferenciaRepositorio = transferenciaRepositorio;
    }

    public async Task<Result<TransferenciaResponseDto>> ExecutarAsync(TransferenciaRequestDto transferencia)
    {
        var pagador = await _carteiraRepositorio.BuscarPorId(transferencia.PagadorId);
        var recebedor = await _carteiraRepositorio.BuscarPorId(transferencia.RecebedorId);

        if (recebedor is null || pagador is null)
            return Result<TransferenciaResponseDto>.Falha("Nenhuma entidade foi encontrada.");
        if (pagador.TipoEntidade == TipoEntidade.Lojista)
            return Result<TransferenciaResponseDto>.Falha("Lojista não pode efetuar transação.");
        if (pagador.SaldoConta < transferencia.Valor)
            return Result<TransferenciaResponseDto>.Falha("Saldo insuficiente.");
        var autorizador = await _autorizadorService.AutorizadorAsync();
        if (autorizador != true)
        {
            return Result<TransferenciaResponseDto>.Falha("Você não está autorizado a realizar essa transação.");
        }
        

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