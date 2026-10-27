namespace DesafioPicPay.Models.Response;

/// <summary>Dados retornados após uma transferência.</summary>
/// <param name="idTransferencia">Identificador da transferência realizada.</param>
/// <param name="idRemetente">Identificador da carteira de origem.</param>
/// <param name="idDestino">Identificador da carteira de destino.</param>
/// <param name="ValorTransferido">Valor transferido.</param>
public record TransferenciaResponseDto(Guid idTransferencia, int idRemetente, int idDestino, decimal ValorTransferido);