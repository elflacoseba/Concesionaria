using Concesionaria.API.Application.Commons;
using FluentValidation.Results;

namespace Concesionaria.API.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<ErrorValidation> Errors { get; }

        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : base("Uno o m�s errores de validaci�n han ocurrido.")
        {
            Errors = new List<ErrorValidation>();
            foreach (var failure in failures)
            {
                Errors.Add(new ErrorValidation(failure.PropertyName, failure.ErrorMessage));
            }
        }
    }
}
