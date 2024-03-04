using BLL.DTO.Tag;
using BLL.DTO.Transaction;
using BLL.Exceptions;
using BLL.Services.Abstracts;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TransactionsService : ContextAccess
    {
        public TransactionsService(SMDbContext context) : base(context)
        {
        }

        public TransactionDTO Create(CreateTransactionDTO dto) 
        {
            Transaction toCreate = dto.Adapt<Transaction>();
            _context.Add(toCreate);
            _context.SaveChanges();

            return toCreate.Adapt<TransactionDTO>();
        }

        public TransactionDTO Get(int id)
        {
            Transaction received = _context.Transactions.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            return received.Adapt<TransactionDTO>();
        }

        public TransactionNestedDTO GetNested(int id) 
        {
            var transaction = _context.Transactions.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            var currency = _context.Currencies.GetById(transaction.CurrencyId)
                    ?? throw new EntityNotFoundException(typeof(Currency));

            var format = _context.MoneyFormats.GetById(transaction.FormatId)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));

            var tags = _context.TagTransactions
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
            Transaction toDelete = _context.Transactions.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            _context.Remove(toDelete);
            _context.SaveChanges();
        }

        public TransactionDTO Update(int id, TransactionUpdateDTO dto) 
        {
            Transaction toUpdate = _context.Transactions.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            toUpdate.Adapt(dto);
            _context.SaveChanges();

            return toUpdate.Adapt<TransactionDTO>();
        }
    }
}
