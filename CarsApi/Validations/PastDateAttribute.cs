using System.ComponentModel.DataAnnotations;

namespace CarsApi.Validations
{
    public class PastDateAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value)
        => value is DateTime date && date < DateTime.Now;
    }
}
