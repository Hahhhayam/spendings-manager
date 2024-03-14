using DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using static BLL.DTO.DTOConstants;

namespace BLL.DTO.MoneyFormat
{
    public class CreateMoneyFormatDTO : IValidatableObject
    {
        [Required]
        [StringLength(MoneyFormatNameMaxLength, MinimumLength = MoneyFormatNameMinLength)]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetRequiredService<SMDbContext>();

            if (context.MoneyFormats.Find(this.Name) != null)
            {
                yield return new ValidationResult("Name already taken");
            }
        }
    }
}
