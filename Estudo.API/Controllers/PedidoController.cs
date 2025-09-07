using Estudo.Service;
using Estudo.Model;

public static class PedidoController
{
    // Recebe RouteGroupBuilder para registro centralizado
    public static void RegisterEndpoints(RouteGroupBuilder group)
    {
        var service = new PedidoService();
        group.MapGet("/", () => service.Listar());
        group.MapGet("/{id}", (int id) => service.BuscarPorId(id));
        group.MapPost("/", (Pedido pedido) => service.Adicionar(pedido));
        group.MapPut("/{id}", (int id, Pedido pedido) =>
        {
            var updated = service.Atualizar(id, pedido);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });
        group.MapDelete("/{id}", (int id) =>
        {
            return service.Remover(id) ? Results.Ok() : Results.NotFound();
        });
    }
}
