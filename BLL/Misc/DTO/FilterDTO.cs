using BLL.Misc.DTO.Currency;
using BLL.Misc.DTO.Tag;

namespace BLL.Misc.DTO
{
    public class FilterDTO
    {
        public DateTime? LowerDate { get; set; }

        public DateTime? UpperDate { get; set; }

        public IEnumerable<TagDTO>? Tags { get; set; }

        public CurrencyDTO? Currency { get; set; }
    }
}