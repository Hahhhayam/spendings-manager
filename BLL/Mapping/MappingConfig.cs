using BLL.DTO.Currency;
using BLL.DTO.Debt;
using BLL.DTO.MoneyFormat;
using BLL.DTO.Person;
using BLL.DTO.Tag;
using BLL.DTO.Transaction;
using DAL.Entities;
using Mapster;

namespace BLL.Mapping
{
    public class MappingConfig : TypeAdapterConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "StyleCop.CSharp.ReadabilityRules",
            "SA1101:Prefix local calls with this",
            Justification = "*this* keyword overuse")]
        public MappingConfig()
        {
            ForType<CreateTransactionDTO, Transaction>()
                .Map(dst => dst.Added, src => DateTime.Now);

            // Write mapping <CreateDebtWithTransactionDTO, CreateDebtDTO>
            // Write mapping <CreateDebtWithTransactionDTO, CreateTransactionDTO>

            // Update mappings
            ForType<TransactionUpdateDTO, Transaction>()
                .IgnoreNullValues(true);

            ForType<PersonUpdateDTO, Person>()
                .IgnoreNullValues(true);

            ForType<CurrencyUpdateDTO, Currency>()
                .IgnoreNullValues(true);

            ForType<MoneyFormatUpdateDTO, MoneyFormat>()
                .IgnoreNullValues(true);

            ForType<TagUpdateDTO, Tag>()
                .IgnoreNullValues(true);

            ForType<DebtUpdateDTO, Debt>()
                .IgnoreNullValues(true);
        }
    }
}
