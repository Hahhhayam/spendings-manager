using BLL.DTO.Currency;
using BLL.DTO.Tag;
using DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class FilterDTO : IValidatableObject
    {
        public DateTime? LowerDate { get; set; }

        public DateTime? UpperDate { get; set; }

        public IEnumerable<TagDTO>? Tags { get; set; }

        public CurrencyDTO? Currency { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetRequiredService<SMDbContext>();

            if (this.LowerDate != null && this.UpperDate != null && this.LowerDate > this.UpperDate) { }
            {
                yield return new ValidationResult("Lower date is later that upper");
            }

            if (this.LowerDate == null
                && this.UpperDate == null
                && this.Tags?.Count() == 0
                && this.Currency == null)
            {
                yield return new ValidationResult("Filters are empty");
            }
        }
    }
}