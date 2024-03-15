using System.ComponentModel.DataAnnotations;
using DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using static BLL.DTO.DTOConstants;

namespace BLL.DTO.Person
{
    public class CreatePersonDTO : IValidatableObject
    {
        [Required]
        [StringLength(PersonNameMaxLength, MinimumLength = PersonNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PersonContactMaxLength, MinimumLength = PersonContactMinLength)]
        public string Contact { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetRequiredService<SMDbContext>();

            if (context.People.Find(this.Name) != null)
            {
                yield return new ValidationResult("Name already taken.");
            }
        }
    }
}
