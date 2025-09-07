using Estudo.Model;
using Estudo.Service;

// Testes unitários para os métodos do ClienteService
public class ClienteControllerTests
{
    [Fact]//É um atributo do xUnit que indica que o método é um teste unitário. O framework executa todos os métodos marcados com [Fact] como testes.
    public void DeveAdicionarClienteComSucesso()
    {
        // Arrange É a etapa onde você prepara o cenário do teste, criando objetos, inicializando variáveis e configurando tudo que será necessário para o teste.
        var service = new ClienteService();
        var novoCliente = new Cliente { Nome = "Teste", Email = "teste@email.com" };

        // Act É a ação principal do teste, onde você executa o método ou funcionalidade que deseja validar.
        var clienteAdicionado = service.Adicionar(novoCliente);

        // Assert  É a verificação do resultado. Aqui você compara o resultado obtido com o esperado, usando métodos como Assert.Equal, Assert.True, Assert.NotNull, etc.
        Assert.NotNull(clienteAdicionado);
        Assert.Equal(1, clienteAdicionado.Id);
        Assert.Single(service.Listar());
    }

    [Fact]
    public void DeveAtualizarClienteComSucesso()
    {
        // Arrange
        var service = new ClienteService();
        var cliente = service.Adicionar(new Cliente { Nome = "Teste", Email = "teste@email.com" });
        var clienteAtualizado = new Cliente { Nome = "Novo", Email = "novo@email.com" };

        // Act
        var resultado = service.Atualizar(cliente.Id, clienteAtualizado);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Novo", resultado.Nome);
        Assert.Equal("novo@email.com", resultado.Email);
    }

    [Fact]
    public void DeveRemoverClienteComSucesso()
    {
        // Arrange
        var service = new ClienteService();
        var cliente = service.Adicionar(new Cliente { Nome = "Teste", Email = "teste@email.com" });

        // Act
        var resultado = service.Remover(cliente.Id);

        // Assert
        Assert.True(resultado);
        Assert.Empty(service.Listar());
    }
}


/*
Principais funções do Assert no xUnit e suas explicações:

- Assert.Equal(expected, actual): Verifica se o valor esperado é igual ao valor atual.
- Assert.NotEqual(notExpected, actual): Verifica se o valor não esperado é diferente do valor atual.
- Assert.True(condition): Verifica se a condição é verdadeira.
- Assert.False(condition): Verifica se a condição é falsa.
- Assert.Null(object): Verifica se o objeto é nulo.
- Assert.NotNull(object): Verifica se o objeto não é nulo.
- Assert.Single(collection): Verifica se a coleção possui exatamente um elemento.
- Assert.Empty(collection): Verifica se a coleção está vazia.
- Assert.Contains(item, collection): Verifica se o item está presente na coleção.
- Assert.DoesNotContain(item, collection): Verifica se o item não está presente na coleção.
- Assert.Throws<TException>(() => action): Verifica se uma exceção do tipo TException é lançada ao executar a ação.

Essas funções ajudam a validar o comportamento esperado dos métodos testados, tornando os testes mais robustos e confiáveis.
*/
