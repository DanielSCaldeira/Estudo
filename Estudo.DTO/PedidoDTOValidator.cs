using FluentValidation;

namespace Estudo.DTO
{
    public class PedidoDTOValidator : AbstractValidator<PedidoDTO>
    {
        public PedidoDTOValidator()
        {
            RuleFor(x => x.ClienteId)
                .NotEmpty().WithMessage("ClienteId é obrigatório.");

            RuleFor(x => x.ValorTotal)
                .GreaterThan(0).WithMessage("ValorTotal deve ser maior que zero.");

            RuleFor(x => x.DataPedido)
                .NotEmpty().WithMessage("DataPedido é obrigatória.");
        }
    }
}