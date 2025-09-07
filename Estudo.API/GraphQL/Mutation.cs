using Estudo.DTO;
using Estudo.Service;
using Estudo.Model;

namespace Estudo.API.GraphQL;

public class Mutation
{
    public Cliente AddCliente(ClienteDTO dto, [Service] ClienteService service)
    {
        var cliente = new Cliente
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Documento = dto.Documento
        };
        return service.Adicionar(cliente);
    }

    public Pedido AddPedido(PedidoDTO dto, [Service] PedidoService service)
    {
        var pedido = new Pedido
        {
            ClienteId = dto.ClienteId,
            ValorTotal = dto.ValorTotal,
            DataPedido = dto.DataPedido
        };
        return service.Adicionar(pedido);
    }
}
/