using RabbitMQ.Client;
using System.Text;

namespace Estudo.RabbitMQ
{
    // Classe responsável por enviar pedidos para a fila RabbitMQ
    public class AdicionarFilaPedido
    {
        private readonly string _hostname = "localhost"; // Endereço do servidor RabbitMQ
        private readonly string _nomeFila = "pedidos";   // Nome da fila de pedidos

        // Envia um pedido (em formato JSON) para a fila
        public void EnviarParaFila(string pedidoJson)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var conexao = factory.CreateConnection(); // Cria conexão com RabbitMQ
            using var canal = conexao.CreateModel();        // Cria canal de comunicação
            canal.QueueDeclare(queue: _nomeFila,
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null); // Garante que a fila existe

            var corpo = Encoding.UTF8.GetBytes(pedidoJson); // Converte pedido para bytes
            canal.BasicPublish(exchange: "",
                               routingKey: _nomeFila,
                               basicProperties: null,
                               body: corpo); // Publica mensagem na fila
        }
    }
}