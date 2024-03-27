namespace BLL.Misc.DTO.Transaction
{
    public class TransactionUpdateDTO
    {
        public string? Name { get; set; }

        public decimal? Amount { get; set; }

        public int? CurrencyId { get; set; }

        public int? FormatId { get; set; }
    }
}
