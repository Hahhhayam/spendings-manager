using System.ComponentModel.DataAnnotations;
using DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using static BLL.DTO.DTOConstants;

namespace BLL.DTO.Transaction
{
    public class CreateTransactionDTO : IValidatableObject
    {
        [Required]
        [StringLength(TransactionNameMaxLength, MinimumLength = TransactionNameMinLength)]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public int FormatId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetRequiredService<SMDbContext>();

            if (context.Currencies.Find(this.CurrencyId) == null)
            {
                yield return new ValidationResult("Currency not found");
            }

            if (context.MoneyFormats.Find(this.FormatId) == null)
            {
                yield return new ValidationResult("Format not found");
            }
        }
    }
}
