using Estudo.DTO;
using Estudo.Model;
using Estudo.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public static class PedidoController
{
    // Recebe RouteGroupBuilder para registro centralizado
    public static void RegisterEndpoints(RouteGroupBuilder group)
    {
        var service = new PedidoService();
        var validator = new PedidoDTOValidator();
        group.MapGet("/", () => service.Listar());
        group.MapGet("/{id}", (int id) => service.BuscarPorId(id));
        group.MapPost("/", ([FromBody] PedidoDTO dto) =>
        {
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(dto, context, results, true))
                return Results.BadRequest(results);
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);
            var pedido = new Pedido
            {
                ClienteId = dto.ClienteId,
                ValorTotal = dto.ValorTotal,
                DataPedido = dto.DataPedido
            };
            return Results.Ok(service.Adicionar(pedido));
        });
        group.MapPut("/{id}", (int id, [FromBody] PedidoDTO dto) =>
        {
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(dto, context, results, true))
                return Results.BadRequest(results);
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);
            var pedido = new Pedido
            {
                ClienteId = dto.ClienteId,
                ValorTotal = dto.ValorTotal,
                DataPedido = dto.DataPedido
            };
            var updated = service.Atualizar(id, pedido);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });
        group.MapDelete("/{id}", (int id) =>
        {
            return service.Remover(id) ? Results.Ok() : Results.NotFound();
        });
    }
}
