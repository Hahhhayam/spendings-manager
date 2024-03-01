using System.ComponentModel.DataAnnotations;
using DAL.Entities.Abstractions;

namespace DAL.Entities
{
    public class Person : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Contact { get; set; } = null!;

        public List<Debt> Debts { get; set; } = null!;
    }
}
