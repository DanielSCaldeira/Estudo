using Estudo.Service;
using Estudo.Model;

public static class ClienteController
{
    // Recebe RouteGroupBuilder para registro centralizado
    public static void RegisterEndpoints(RouteGroupBuilder group)
    {
        var service = new ClienteService();
        // Todos os endpoints exigem autenticação JWT
        group.MapGet("/", () => 
            service.Listar()
        ).RequireAuthorization();
        group.MapGet("/{id}", (int id) => service.BuscarPorId(id)).RequireAuthorization();
        group.MapPost("/", (Cliente cliente) => service.Adicionar(cliente)).RequireAuthorization();
        group.MapPut("/{id}", (int id, Cliente cliente) => {
            var updated = service.Atualizar(id, cliente);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        }).RequireAuthorization();
        group.MapDelete("/{id}", (int id) => {
            return service.Remover(id) ? Results.Ok() : Results.NotFound();
        }).RequireAuthorization();
    }
}
