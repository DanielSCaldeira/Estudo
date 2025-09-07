namespace Estudo.Model;

public class Pedido
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public decimal ValorTotal { get; set; } // Adicionado para compatibilidade com DTO
    public DateTime DataPedido { get; set; } // Adicionado para compatibilidade com DTO
}