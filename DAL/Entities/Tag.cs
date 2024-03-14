using System.ComponentModel.DataAnnotations;
using DAL.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Tag : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        public List<TagTransaction> TagTransactions { get; set; } = null!;
    }
}
