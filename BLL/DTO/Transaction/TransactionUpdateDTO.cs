using System.ComponentModel.DataAnnotations;
using DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using static BLL.DTO.DTOConstants;

namespace BLL.DTO.Transaction
{
    public class TransactionUpdateDTO
    {
        [StringLength(TransactionNameMaxLength, MinimumLength = TransactionNameMinLength)]
        public string? Name { get; set; }

        public decimal? Amount { get; set; }

        public int? CurrencyId { get; set; }

        public int? FormatId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetRequiredService<SMDbContext>();

            if (this.CurrencyId != null && context.Currencies.Find(this.CurrencyId) == null)
            {
                yield return new ValidationResult("Currency not found");
            }

            if (this.FormatId != null && context.MoneyFormats.Find(this.FormatId) == null)
            {
                yield return new ValidationResult("Format not found");
            }
        }

    }
}
