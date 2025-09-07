using Estudo.Model;
using Estudo.Service;
using Xunit;
using System;

// Testes unitários para os métodos do PedidoService
public class PedidoControllerTests
{
    [Fact]
    public void DeveAdicionarPedidoComSucesso()
    {
        // Arrange
        var service = new PedidoService();
        var novoPedido = new Pedido { Data = DateTime.Now, ClienteId = 1, Cliente = new Cliente { Id = 1, Nome = "Teste", Email = "teste@email.com" } };

        // Act
        var pedidoAdicionado = service.Adicionar(novoPedido);

        // Assert
        Assert.NotNull(pedidoAdicionado);
        Assert.Equal(1, pedidoAdicionado.Id);
        Assert.Single(service.Listar());
    }

    [Fact]
    public void DeveAtualizarPedidoComSucesso()
    {
        // Arrange
        var service = new PedidoService();
        var pedido = service.Adicionar(new Pedido { Data = DateTime.Now, ClienteId = 1, Cliente = new Cliente { Id = 1, Nome = "Teste", Email = "teste@email.com" } });
        var pedidoAtualizado = new Pedido { Data = DateTime.Now.AddDays(1), ClienteId = 2, Cliente = new Cliente { Id = 2, Nome = "Novo", Email = "novo@email.com" } };

        // Act
        var resultado = service.Atualizar(pedido.Id, pedidoAtualizado);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(pedidoAtualizado.Data, resultado.Data);
        Assert.Equal(2, resultado.ClienteId);
        Assert.Equal("Novo", resultado.Cliente.Nome);
    }

    [Fact]
    public void DeveRemoverPedidoComSucesso()
    {
        // Arrange
        var service = new PedidoService();
        var pedido = service.Adicionar(new Pedido { Data = DateTime.Now, ClienteId = 1, Cliente = new Cliente { Id = 1, Nome = "Teste", Email = "teste@email.com" } });

        // Act
        var resultado = service.Remover(pedido.Id);

        // Assert
        Assert.True(resultado);
        Assert.Empty(service.Listar());
    }
}

/*
Agora os testes validam diretamente a regra de negócio centralizada no PedidoService,
permitindo reaproveitamento da lógica tanto na API quanto nos testes.
*/
