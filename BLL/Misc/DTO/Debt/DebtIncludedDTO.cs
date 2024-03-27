using BLL.Misc.DTO.Tag;

namespace BLL.Misc.DTO.Debt
{
    public class DebtIncludedDTO : DebtDTO
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public int? CurrencyId { get; set; }

        public string? CurrencyName { get; set; }

        public int? FormatId { get; set; }

        public string? FormatName { get; set; }

        public DateTime Added { get; set; }

        public List<TagDTO> Tags { get; set; }

        public string PersonName { get; set; }

        public string PersonContact { get; set; }
    }
}
