using System.ComponentModel.DataAnnotations;
using DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using static BLL.DTO.DTOConstants;

namespace BLL.DTO.Tag
{
    public class CreateTagDTO : IValidatableObject
    {
        [Required]
        [StringLength(TagNameMaxLength, MinimumLength = TagNameMinLength)]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = validationContext.GetRequiredService<SMDbContext>();

            if (context.Tags.Find(this.Name) != null)
            {
                yield return new ValidationResult("Name already taken");
            }
        }
    }
}
