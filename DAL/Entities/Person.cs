using System.ComponentModel.DataAnnotations;
using DAL.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Person : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Contact { get; set; } = null!;

        public List<Debt> Debts { get; set; } = null!;
    }
}
