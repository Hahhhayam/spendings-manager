using DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Debt
{
    public class CreateDebtDTO : IValidatableObject
    {
        [Required]
        public int TransactionId { get; set; }

        public int? PersonId { get; set; }

        [Required]
        public DateTime ToClose { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetRequiredService<SMDbContext>();

            if (context.Transactions.Find(this.TransactionId) == null)
            {
                yield return new ValidationResult("Transaction not exist.");
            }

            if (this.PersonId != null && context.People.Find(this.PersonId) == null)
            {
                yield return new ValidationResult("Person not exist.");
            }
        }
    }
}
