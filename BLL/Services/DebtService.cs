using System.ComponentModel.DataAnnotations;
using BLL.Misc.DTO.Debt;
using BLL.Misc.Exceptions;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class DebtService
    {
        private readonly SpendingsManagerDbContext context;

        public DebtService(SpendingsManagerDbContext context)
        {
            this.context = context;
        }

        public DebtDTO Create(CreateDebtDTO dto)
        {
            Debt toCreate = dto.Adapt<Debt>();

            this.context.Add(toCreate);
            this.context.SaveChanges();

            return toCreate.Adapt<DebtDTO>();
        }

        public DebtDTO CreateWithTransaction(CreateDebtWithTransactionDTO dto)
        {
            Transaction toCreateAndAllocate = dto.Adapt<Transaction>();

            Debt toCreate = dto.Adapt<Debt>();

            using (var t = this.context.Database.BeginTransaction())
            {
                this.context.Transactions.Add(toCreateAndAllocate);
                this.context.SaveChanges();

                toCreate.TransactionId = toCreateAndAllocate.Id;

                this.context.Debt.Add(toCreate);
                this.context.SaveChanges();

                t.Commit();
            }

            return toCreate.Adapt<DebtDTO>();
        }

        public DebtDTO Get(int id)
        {
            Debt received = this.context.Debt.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Debt));

            return received.Adapt<DebtDTO>();
        }

        public DebtIncludedDTO GetIncluded(int id)
        {
            Debt received = this.context.Debt
                .AsNoTracking()
                .Include(d => d.Transaction.Currency)
                .Include(d => d.Transaction.Format)
                .Include(d => d.Transaction.TagTransactions)
                        .ThenInclude(tT => tT.Tag)
                .Include(d => d.Person)
                .GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Debt));

            return received.Adapt<DebtIncludedDTO>();
        }

        public IEnumerable<DebtDTO> GetAll()
        {
            return this.context.Debt
                .AsNoTracking()
                .ProjectToType<DebtDTO>();
        }

        public IEnumerable<DebtIncludedDTO> GetAllIncluded()
        {
            return this.context.Debt
                .AsNoTracking()
                .Include(d => d.Transaction.Currency)
                .Include(d => d.Transaction.Format)
                .Include(d => d.Transaction.TagTransactions)
                        .ThenInclude(tT => tT.Tag)
                .Include(d => d.Person)
                .ProjectToType<DebtIncludedDTO>();
        }

        public void Delete(int id)
        {
            Debt toDelete = this.context.Debt.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Debt));
            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }

        public DebtDTO Update(int id, DebtUpdateDTO dto)
        {
            Debt toUpdate = this.context.Debt.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Debt));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<DebtDTO>();
        }
    }
}
