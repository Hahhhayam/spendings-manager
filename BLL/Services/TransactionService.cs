using System.ComponentModel.DataAnnotations;
using System.Linq;
using BLL.DTO.Currency;
using BLL.DTO.MoneyFormat;
using BLL.DTO.Tag;
using BLL.DTO.Transaction;
using BLL.Exceptions;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TransactionService
    {
        private readonly SMDbContext context;

        public TransactionService(SMDbContext context)
        {
            this.context = context;
        }

        public TransactionDTO Create(CreateTransactionDTO dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto));

            Transaction toCreate = dto.Adapt<Transaction>();

            using (var t = this.context.Database.BeginTransaction())
            {
                this.context.Add(toCreate);
                this.context.SaveChanges();

                foreach (int tagId in dto.TagIds)
                {
                    this.context.TagTransactions.Add(new TagTransaction()
                    {
                        TagId = tagId,
                        TransactionId = toCreate.Id,
                    });
                }

                this.context.SaveChanges();
                t.Commit();
            }

            return toCreate.Adapt<TransactionDTO>();
        }

        public MoneyFormatDTO CreateFormat(CreateMoneyFormatDTO dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto));

            MoneyFormat toCreate = dto.Adapt<MoneyFormat>();

            this.context.Add(toCreate);
            this.context.SaveChanges();

            return toCreate.Adapt<MoneyFormatDTO>();
        }

        public CurrencyDTO CreateCurrency(CreateCurrencyDTO dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto));

            Currency toCreate = dto.Adapt<Currency>();

            this.context.Add(toCreate);
            this.context.SaveChanges();

            return toCreate.Adapt<CurrencyDTO>();
        }

        public TagDTO CreateTag(CreateTagDTO dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto));

            Tag toCreate = dto.Adapt<Tag>();
            this.context.Add(toCreate);
            this.context.SaveChanges();

            return toCreate.Adapt<TagDTO>();
        }

        public void AddTag(int id, int tagId)
        {
            _ = this.context.Transactions
                .Include(t => t.TagTransactions)
                    .ThenInclude(tT => tT.Tag)
                .GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            _ = this.context.Tags
                .GetById(tagId)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            this.context.TagTransactions.Add(new TagTransaction()
            {
                TagId = tagId,
                TransactionId = id,
            });

            this.context.SaveChanges();
        }

        public TransactionDTO Get(int id)
        {
            Transaction received = this.context.Transactions
                .GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            return received.Adapt<TransactionDTO>();
        }

        public TransactionIncludedDTO GetIncluded(int id)
        {
            Transaction received = this.context.Transactions
                .Include(t => t.Currency)
                .Include(t => t.Format)
                .Include(t => t.TagTransactions)
                    .ThenInclude(tT => tT.Tag)
                .GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));

            return received.Adapt<TransactionIncludedDTO>();
        }

        public IEnumerable<TransactionDTO> GetAll()
        {
            return this.context.Transactions
                .AsNoTracking()
                .ProjectToType<TransactionDTO>();
        }

        public IEnumerable<TagDTO> GetAllTags()
        {
            return this.context.Tags
                .AsNoTracking()
                .ProjectToType<TagDTO>();
        }

        public IEnumerable<CurrencyDTO> GetAllCurrency()
        {
            return this.context.Currencies
                .AsNoTracking()
                .ProjectToType<CurrencyDTO>();
        }

        public IEnumerable<MoneyFormatDTO> GetAllFormats()
        {
            return this.context.MoneyFormats
                .AsNoTracking()
                .ProjectToType<MoneyFormatDTO>();
        }

        public IEnumerable<TransactionIncludedDTO> GetAllIncluded()
        {
            return this.context.Transactions
                .AsNoTracking()
                .Include(t => t.Currency)
                .Include(t => t.Format)
                .Include(t => t.TagTransactions)
                    .ThenInclude(tT => tT.Tag)
                .ProjectToType<TransactionIncludedDTO>();
        }

        public TransactionDTO Update(int id, TransactionUpdateDTO dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto));

            Transaction toUpdate = this.context.Transactions.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<TransactionDTO>();
        }

        public MoneyFormatDTO UpdateFormat(int id, MoneyFormatUpdateDTO dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto));

            MoneyFormat toUpdate = this.context.MoneyFormats.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<MoneyFormatDTO>();
        }

        public CurrencyDTO UpdateCurrency(int id, CurrencyUpdateDTO dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto));

            Currency toUpdate = this.context.Currencies.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Currency));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<CurrencyDTO>();
        }

        public TagDTO UpdateTag(int id, TagUpdateDTO dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto));

            Tag toUpdate = this.context.Tags.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Tag));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<TagDTO>();
        }

        public void Delete(int id)
        {
            Transaction toDelete = this.context.Transactions.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Transaction));
            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }

        public void DeleteFormat(int id)
        {
            MoneyFormat toDelete = this.context.MoneyFormats.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));
            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }

        public void DeleteCurrency(int id)
        {
            Currency toDelete = this.context.Currencies.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Currency));
            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }

        public void DeleteTag(int id)
        {
            Tag toDelete = this.context.Tags.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Tag));
            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }
    }
}
