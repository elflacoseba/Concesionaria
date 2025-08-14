namespace Concesionaria.Application.Commons
{
    public class ErrorValidation
    {
        public ErrorValidation(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string? PropertyName { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string? ErrorMessage { get; set; }

    }
}
