using Estudo.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Estudo.Service
{
    public class PedidoService
    {
        private readonly List<Pedido> _pedidos = new();

        public IEnumerable<Pedido> Listar() => _pedidos;

        public Pedido? BuscarPorId(int id) => _pedidos.FirstOrDefault(p => p.Id == id);

        public Pedido Adicionar(Pedido pedido)
        {
            pedido.Id = _pedidos.Count + 1;
            _pedidos.Add(pedido);
            return pedido;
        }

        public Pedido? Atualizar(int id, Pedido pedido)
        {
            var existing = _pedidos.FirstOrDefault(p => p.Id == id);
            if (existing is null) return null;
            existing.Data = pedido.Data;
            existing.ClienteId = pedido.ClienteId;
            existing.Cliente = pedido.Cliente;
            return existing;
        }

        public bool Remover(int id)
        {
            var pedido = _pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido is null) return false;
            _pedidos.Remove(pedido);
            return true;
        }
    }
}
