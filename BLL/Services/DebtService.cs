using System.ComponentModel.DataAnnotations;
using BLL.DTO.Debt;
using BLL.Exceptions;
using BLL.Services.Abstracts;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class DebtService : ContextAccess
    {
        private ValidationContext validationContext { get; set; }

        public DebtService(SMDbContext context)
            : base(context)
        {
        }

        public DebtDTO Create(CreateDebtDTO dto)
        {
            this.validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, this.validationContext);

            Debt toCreate = dto.Adapt<Debt>();

            this.context.Add(toCreate);
            this.context.SaveChanges();

            return toCreate.Adapt<DebtDTO>();
        }

        public DebtDTO Get(int id)
        {
            Debt received = this.context.Debt.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Debt));

            return received.Adapt<DebtDTO>();
        }

        public IList<DebtDTO> GetAll()
        {
            List<DebtDTO> result = new ();
            foreach (var tag in this.context.Debt.AsNoTracking().ToList())
            {
                result.Add(tag.Adapt<DebtDTO>());
            }

            return result;
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
            this.validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, this.validationContext);

            Debt toUpdate = this.context.Debt.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Debt));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<DebtDTO>();
        }
    }
}
