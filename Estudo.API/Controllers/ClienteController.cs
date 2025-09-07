using Estudo.DTO;
using Estudo.Model;
using Estudo.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public static class ClienteController
{
    // Recebe RouteGroupBuilder para registro centralizado
    public static void RegisterEndpoints(RouteGroupBuilder group)
    {
        var service = new ClienteService();
        var validator = new ClienteDTOValidator();
        // Todos os endpoints exigem autenticação JWT
        group.MapGet("/", () => 
            service.Listar()
        ).RequireAuthorization();
        group.MapGet("/{id}", (int id) => service.BuscarPorId(id)).RequireAuthorization();
        group.MapPost("/", ([FromBody] ClienteDTO dto) =>
        {
            // DataAnnotations
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(dto, context, results, true))
                return Results.BadRequest(results);
            // FluentValidation
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Documento = dto.Documento
            };
            return Results.Ok(service.Adicionar(cliente));
        }).RequireAuthorization();
        group.MapPut("/{id}", (int id, [FromBody] ClienteDTO dto) =>
        {
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(dto, context, results, true))
                return Results.BadRequest(results);
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Documento = dto.Documento
            };
            var updated = service.Atualizar(id, cliente);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        }).RequireAuthorization();
        group.MapDelete("/{id}", (int id) => {
            return service.Remover(id) ? Results.Ok() : Results.NotFound();
        }).RequireAuthorization();
    }
}
