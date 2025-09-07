using FluentValidation;

namespace Estudo.DTO
{
    public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
    {
        public ClienteDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("Documento é obrigatório.")
                .Length(11, 14).WithMessage("Documento deve ter entre 11 e 14 caracteres.");
        }
    }
}