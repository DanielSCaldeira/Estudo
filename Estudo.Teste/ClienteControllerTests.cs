using Estudo.Model;
using Estudo.Service;

// Testes unit�rios para os m�todos do ClienteService
public class ClienteControllerTests
{
    [Fact]//� um atributo do xUnit que indica que o m�todo � um teste unit�rio. O framework executa todos os m�todos marcados com [Fact] como testes.
    public void DeveAdicionarClienteComSucesso()
    {
        // Arrange � a etapa onde voc� prepara o cen�rio do teste, criando objetos, inicializando vari�veis e configurando tudo que ser� necess�rio para o teste.
        var service = new ClienteService();
        var novoCliente = new Cliente { Nome = "Teste", Email = "teste@email.com" };

        // Act � a a��o principal do teste, onde voc� executa o m�todo ou funcionalidade que deseja validar.
        var clienteAdicionado = service.Adicionar(novoCliente);

        // Assert  � a verifica��o do resultado. Aqui voc� compara o resultado obtido com o esperado, usando m�todos como Assert.Equal, Assert.True, Assert.NotNull, etc.
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
Principais fun��es do Assert no xUnit e suas explica��es:

- Assert.Equal(expected, actual): Verifica se o valor esperado � igual ao valor atual.
- Assert.NotEqual(notExpected, actual): Verifica se o valor n�o esperado � diferente do valor atual.
- Assert.True(condition): Verifica se a condi��o � verdadeira.
- Assert.False(condition): Verifica se a condi��o � falsa.
- Assert.Null(object): Verifica se o objeto � nulo.
- Assert.NotNull(object): Verifica se o objeto n�o � nulo.
- Assert.Single(collection): Verifica se a cole��o possui exatamente um elemento.
- Assert.Empty(collection): Verifica se a cole��o est� vazia.
- Assert.Contains(item, collection): Verifica se o item est� presente na cole��o.
- Assert.DoesNotContain(item, collection): Verifica se o item n�o est� presente na cole��o.
- Assert.Throws<TException>(() => action): Verifica se uma exce��o do tipo TException � lan�ada ao executar a a��o.

Essas fun��es ajudam a validar o comportamento esperado dos m�todos testados, tornando os testes mais robustos e confi�veis.
*/
