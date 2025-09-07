using System;
using System.ComponentModel.DataAnnotations;

namespace Estudo.DTO
{
    public class PedidoDTO
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal ValorTotal { get; set; }

        [Required]
        public DateTime DataPedido { get; set; }
    }
}