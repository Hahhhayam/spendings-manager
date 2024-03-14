using BLL.DTO.Tag;
using BLL.DTO.Transaction;
using BLL.Exceptions;
using BLL.Services.Abstracts;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BLL.Services
{
    public class TransactionsService : ContextAccess
    {
        private readonly ValidationContext validationContext;

        public TransactionsService(
            SMDbContext context,
            ValidationContext validationContext)
            : base(context)
        {
            this.validationContext = validationContext;
        }

        public TransactionDTO Create(CreateTransactionDTO dto)
        {
            Validator.ValidateObject(dto, this.validationContext);

            Transaction toCreate = dto.Adapt<Transaction>();

            this.context.Add(toCreate);
            this.context.SaveChanges();

            return toCreate.Adapt<TransactionDTO>();
        }

        public TransactionDTO Get(int id)
        {
            Transaction received = this.context.Transactions.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            return received.Adapt<TransactionDTO>();
        }

        public TransactionNestedDTO GetNested(int id)
        {
            var transaction = this.context.Transactions.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            var currency = this.context.Currencies.GetById(transaction.CurrencyId)
                    ?? throw new EntityNotFoundException(typeof(Currency));

            var format = this.context.MoneyFormats.GetById(transaction.FormatId)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));

            var tags = this.context.TagTransactions
                .Where(tt => tt.TransactionId == id)
                .Include(tt => tt.Tag)
                .Select(tt => tt.Tag.Adapt<TagDTO>())
                .ToList();

            return new TransactionNestedDTO()
            {
                Id = transaction.Id,
                Name = transaction.Name,
                Amount = transaction.Amount,
                CurrencyId = currency.Id,
                CurrencyName = currency.Name,
                FormatId = format.Id,
                FormatName = format.Name,
                Added = transaction.Added,
                Tags = tags,
            };
        }

        public void Delete(int id)
        {
            Transaction toDelete = this.context.Transactions.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));
            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }

        public TransactionDTO Update(int id, TransactionUpdateDTO dto)
        {
            Validator.ValidateObject(dto, this.validationContext);

            Transaction toUpdate = context.Transactions.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<TransactionDTO>();
        }
    }
}
