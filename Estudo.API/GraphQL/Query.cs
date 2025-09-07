using Estudo.DTO;
using Estudo.Service;
using Estudo.Model;

namespace Estudo.API.GraphQL;

public class Query
{
    public IEnumerable<Cliente> GetClientes([Service] ClienteService service) => service.Listar();
    public IEnumerable<Pedido> GetPedidos([Service] PedidoService service) => service.Listar();
}
