using System.ComponentModel.DataAnnotations;
using DAL.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Acronym), IsUnique = true)]
    public class Currency : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Acronym { get; set; } = null!;

        public List<Transaction> Transactions { get; set; } = null!;
    }
}
