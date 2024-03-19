using BLL.DTO.Transaction;
using DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Debt
{
    public class CreateDebtWithTransactionDTO : IValidatableObject
    {
        [Required]
        public CreateTransactionDTO Transaction { get; set; }

        public int? PersonId { get; set; }

        [Required]
        public DateTime ToClose { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            this.Transaction.Validate(validationContext);

            var context = validationContext.GetRequiredService<SMDbContext>();

            if (this.PersonId != null && context.People.Find(this.PersonId) == null)
            {
                yield return new ValidationResult("Person not exist.");
            }
        }
    }
}
