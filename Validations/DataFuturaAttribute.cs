using System.ComponentModel.DataAnnotations;

namespace RestauranteReserva.Validations  // onde ficarão essas validações customizadas (DataFuturaAttribute)
{
    public class DataFuturaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)  // retorno do tipo ValidationResult?
        {
            if (value is DateTime data && data < DateTime.Today)
                return new ValidationResult("A data da reserva deve ser hoje ou no futuro.");

            return ValidationResult.Success;
        }
    }
}