namespace DesafioPicPay.Models.Response;

public record TransferenciaResponseDto(Guid idTransferencia, int idRemetente, int idDestino, decimal ValorTransferido);