using System.ComponentModel.DataAnnotations;
using DAL.Context;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.DTO.Debt
{
    public class DebtUpdateDTO : IValidatableObject
    {
        public int? PersonId { get; set; }

        public DateTime? ToClose { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetRequiredService<SMDbContext>();

            if (this.PersonId != null && context.People.Find(this.PersonId) == null)
            {
                yield return new ValidationResult("Person not exist.");
            }
        }
    }
}
