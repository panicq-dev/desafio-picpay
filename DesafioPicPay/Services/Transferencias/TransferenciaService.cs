using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Infra.Repositorios.Transferencias;
using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;
using DesafioPicPay.Services.Notificacoes;
using DesafioPicPay.Services.Autorizador;

namespace DesafioPicPay.Services.Transferencias;

public class TransferenciaService : ITransferenciaService
{

    private readonly ICarteiraRepositorio _carteiraRepositorio;
    private readonly ITransferenciaRepositorio _transferenciaRepositorio;
    private readonly INotificacaoService _notificacaoService;
    private readonly HttpClient _http;

    private readonly IAutorizadorService _autorizadorService;
    public TransferenciaService(ICarteiraRepositorio carteiraRepositorio,
        ITransferenciaRepositorio transferenciaRepositorio, IAutorizadorService autorizadorService, INotificacaoService notificacaoService, HttpClient htpp)
    {
        _autorizadorService = autorizadorService;
        _carteiraRepositorio = carteiraRepositorio;
        _transferenciaRepositorio = transferenciaRepositorio;
        _notificacaoService = notificacaoService;
        _http = htpp;
    }

    public async Task<Result<TransferenciaResponseDto>> ExecutarAsync(TransferenciaRequestDto transferencia)
    {
        // Autorização.
        var autorizador = await _autorizadorService.AutorizadorAsync();
        if (autorizador != true)
        {
            return Result<TransferenciaResponseDto>.Falha("Você não está autorizado a realizar essa transação.");
        }

        // Buscando pagador e recebedor pelo ID.
        var pagador = await _carteiraRepositorio.BuscarPorId(transferencia.PagadorId);
        var recebedor = await _carteiraRepositorio.BuscarPorId(transferencia.RecebedorId);

        // REGRA DE NEGÓCIO: Verificações.
        if (recebedor is null || pagador is null)
            return Result<TransferenciaResponseDto>.Falha("Nenhuma entidade foi encontrada.");

        // Verificação de tipo de entidade.
        if (pagador.TipoEntidade == TipoEntidade.Lojista)
            return Result<TransferenciaResponseDto>.Falha("Lojista não pode efetuar transação.");
        
        // Verificações de valor.
        if (pagador.SaldoConta < transferencia.Valor || pagador.SaldoConta <= 0)
            return Result<TransferenciaResponseDto>.Falha("Essa transação não pode ser realizada! Verifique o saldo.");

        if (transferencia.Valor <= 0)
        {
            return Result<TransferenciaResponseDto>.Falha("Valor de transferência invalido.");
        }
        // Após as verificações, atualizando saldo da conta.
        pagador.DebitarSaldo(transferencia.Valor);
        recebedor.CreditarSaldo(transferencia.Valor);

        // Iniciando a transferência no banco de dados.
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

        // Envio de notificação para o pagador e recebedor.
        _notificacaoService.EnviarNotificacao();

        var response = new TransferenciaResponseDto(transacao.IdTransferencia, pagador.Id, recebedor.Id, transferencia.Valor);
        return Result<TransferenciaResponseDto>.Sucesso(response, "Transferencia realizada com sucesso.");

    }

}