using FluentValidation;

namespace Estudo.DTO
{
    public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
    {
        public ClienteDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome � obrigat�rio.")
                .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email � obrigat�rio.")
                .EmailAddress().WithMessage("Email inv�lido.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("Documento � obrigat�rio.")
                .Length(11, 14).WithMessage("Documento deve ter entre 11 e 14 caracteres.");
        }
    }
}