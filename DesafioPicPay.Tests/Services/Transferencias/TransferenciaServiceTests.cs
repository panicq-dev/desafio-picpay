using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Infra.Repositorios.Transferencias;
using DesafioPicPay.Models.Response;
using Moq;
using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Services.Autorizador;
using DesafioPicPay.Services.Carteiras;
using DesafioPicPay.Services.Notificacoes;
using DesafioPicPay.Services.Transferencias;

namespace xUnitPicPay.Services.Transferencias;

public class TransferenciaServiceTests
{
    [Theory]
    [InlineData(1, 1000)]
    [InlineData(1000, 2)]
    [InlineData(1, -3)]
    [InlineData(0, 1000)]
    public async Task ExecutarAsync_QuandoIdEntidadeForInexistente_DeveRetornarFalha(int pagadorId, int recebedorId)
    {
        // Arrange
        // Criando autorizador, notificação e repositórios simulados. 
        var autorizador = new Mock<IAutorizadorService>();
        var notificacao = new Mock<INotificacaoService>();
        var carteiraRepositorio = new Mock<ICarteiraRepositorio>();
        var transferenciaRepositorio = new Mock<ITransferenciaRepositorio>();

        
        // Simulando respostas.
        autorizador.Setup(a => a.AutorizadorAsync())
            .ReturnsAsync(true);
        
        // Criando pagador e recebedor, junto com as simulações de resposta.
        var pagador = new CarteiraEntidade
        {
            Id = 1,
            SaldoConta = 500m
        };

        carteiraRepositorio.Setup(c => c.BuscarPorId(1))
            .ReturnsAsync(pagador);

        var recebedor = new CarteiraEntidade
        {
            Id = 2,
            SaldoConta = 100m
        };
        
        carteiraRepositorio.Setup(c => c.BuscarPorId(2))
            .ReturnsAsync(recebedor);

        
        // Criando uma transferência com parametro invalido.
        var transferenciaRequest = new TransferenciaRequestDto()
        {
            PagadorId = pagadorId,
            RecebedorId = recebedorId,
            Valor = 50
        };

        // Criando, com as simulações, o serviço de transferência.
        var servicoTransferencia = new TransferenciaService(
            carteiraRepositorio.Object,
            transferenciaRepositorio.Object,
            autorizador.Object,
            notificacao.Object,
            new HttpClient()
            );
        
        // Act
        var resposta = await servicoTransferencia.ExecutarAsync(transferenciaRequest);
        string respostaEsperada = "Nenhuma entidade foi encontrada.";
        
        // Assert
        Assert.False(resposta.EhSucesso);
        Assert.Equal(respostaEsperada, resposta.Mensagem);
    }
}