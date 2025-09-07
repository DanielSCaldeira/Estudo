using Estudo.Model;
using System.Collections.Generic;
using System.Linq;

namespace Estudo.Service
{
    public class ClienteService
    {
        private readonly List<Cliente> _clientes = new();

        public IEnumerable<Cliente> Listar() => _clientes;

        public Cliente? BuscarPorId(int id) => _clientes.FirstOrDefault(c => c.Id == id);

        public Cliente Adicionar(Cliente cliente)
        {
            cliente.Id = _clientes.Count + 1;
            _clientes.Add(cliente);
            return cliente;
        }

        public Cliente? Atualizar(int id, Cliente cliente)
        {
            var existing = _clientes.FirstOrDefault(c => c.Id == id);
            if (existing is null) return null;
            existing.Nome = cliente.Nome;
            existing.Email = cliente.Email;
            return existing;
        }

        public bool Remover(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente is null) return false;
            _clientes.Remove(cliente);
            return true;
        }
    }
}
