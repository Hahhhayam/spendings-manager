using System.ComponentModel.DataAnnotations;
using DAL.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class MoneyFormat : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        public List<Transaction> Transactions { get; set; } = null!;
    }
}
