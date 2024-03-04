using Mapster;
using DAL.Entities;
using BLL.DTO.Person;
using BLL.DTO.Transaction;
using BLL.DTO.Currency;
using BLL.DTO.MoneyFormat;
using BLL.DTO.Tag;
using BLL.DTO.Debt;

namespace BLL.Mapping
{
    public class MappingConfig : TypeAdapterConfig
    {
        public MappingConfig()
        {
            ForType<CreateTransactionDTO, Transaction>()
                .Map(dest => dest.Added, src => DateTime.Now);


            //Update mappings
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
