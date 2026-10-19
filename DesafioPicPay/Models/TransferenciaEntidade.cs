namespace DesafioPicPay.Models;

public class TransferenciaEntidade
{
    public Guid IdTransferencia  { get; set; }
    public int IdRemetente  { get; set; }
    public CarteiraEntidade  CarteiraRemetente { get; set; }
    
    public int IdDestino { get; set; }
    public CarteiraEntidade CarteiraDestinatario { get; set; }
    
    public decimal Valor { get; set; }
    public TransferenciaEntidade(){}
    
    public TransferenciaEntidade(int idRemetente, int idDestino, decimal valor)
    {
        IdTransferencia = Guid.NewGuid();
        IdDestino = idDestino;
        IdRemetente = idRemetente;
        Valor = valor;
    }
    
}