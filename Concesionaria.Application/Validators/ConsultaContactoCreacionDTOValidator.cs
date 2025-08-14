using Concesionaria.Application.DTOs;
using FluentValidation;

namespace Concesionaria.Application.Validators
{
    public class ConsultaContactoCreacionDTOValidator :AbstractValidator<ConsultaContactoCreacionDTO>
    {
        public ConsultaContactoCreacionDTOValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El nombre es requerido.")
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");
            RuleFor(x => x.Email)
                .NotNull().WithMessage("El email es requerido.")
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.")
                .MaximumLength(100).WithMessage("El email no puede exceder los 100 caracteres.");
            RuleFor(x => x.Mensaje)
                .NotNull().WithMessage("El mensaje es requerido.")
                .NotEmpty().WithMessage("El mensaje es obligatorio.")
                .MaximumLength(2000).WithMessage("El mensaje no puede exceder los 2000 caracteres.");
        }
    }
}
