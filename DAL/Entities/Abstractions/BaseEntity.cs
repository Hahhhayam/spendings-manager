using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.Abstractions
{
    [NotMapped]
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
