using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Estudo.Service;
using Estudo.Model;
using System.Text.Json;

namespace Estudo.RabbitMQ
{
    // Classe responsável por consumir pedidos da fila RabbitMQ
    public class ConsumirFilaPedido
    {
        private readonly string _hostname = "localhost"; // Endereço do servidor RabbitMQ
        private readonly string _nomeFila = "pedidos";   // Nome da fila de pedidos
        private readonly PedidoService _pedidoService;

        // Permite injetar PedidoService
        public ConsumirFilaPedido(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // Consome mensagens da fila e adiciona pedido via service
        public void ConsumirFila()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            var conexao = factory.CreateConnection(); // Cria conexão com RabbitMQ
            var canal = conexao.CreateModel();        // Cria canal de comunicação
            canal.QueueDeclare(queue: _nomeFila,
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null); // Garante que a fila existe

            var consumidor = new EventingBasicConsumer(canal);
            consumidor.Received += (model, ea) =>
            {
                var corpo = ea.Body.ToArray(); // Recebe os bytes da mensagem
                var pedidoJson = Encoding.UTF8.GetString(corpo); // Converte para string
                var pedido = JsonSerializer.Deserialize<Pedido>(pedidoJson); // Desserializa para Pedido
                if (pedido != null)
                {
                    _pedidoService.Adicionar(pedido); // Adiciona pedido via service
                }
            };
            canal.BasicConsume(queue: _nomeFila,
                               autoAck: true,
                               consumer: consumidor); // Inicia consumo da fila
        }
    }
}
