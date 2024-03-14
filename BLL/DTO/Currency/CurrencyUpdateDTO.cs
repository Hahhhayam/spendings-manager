using System.ComponentModel.DataAnnotations;
using DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using static BLL.DTO.DTOConstants;

namespace BLL.DTO.Currency
{
    public class CurrencyUpdateDTO : IValidatableObject
    {
        [StringLength(CurrencyNameMaxLength, MinimumLength = CurrencyNameMinLength)]
        public string? Name { get; set; }

        [StringLength(CurrencyAcronymMaxLength, MinimumLength = CurrencyAcronymMinLength)]
        public string? Acronym { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetRequiredService<SMDbContext>();

            if (this.Name != null && context.Currencies.Find(this.Name) != null)
            {
                yield return new ValidationResult("Name already taken");
            }

            if (this.Acronym != null && context.Currencies.Find(this.Acronym) != null)
            {
                yield return new ValidationResult("Acronym already taken");
            }
        }
    }
}
