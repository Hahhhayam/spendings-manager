using BLL.DTO.Tag;

namespace BLL.DTO.Transaction
{
    public class TransactionNestedDTO
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int? CurrencyId { get; set; }

        public string CurrencyName { get; set; }

        public int? FormatId { get; set; }

        public string FormatName { get; set; }

        public string Name { get; set; }

        public DateTime Added { get; set; }

        public List<TagDTO> Tags { get; set; }

    }
}
