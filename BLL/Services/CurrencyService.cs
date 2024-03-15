using System.ComponentModel.DataAnnotations;
using BLL.DTO.Currency;
using BLL.Exceptions;
using BLL.Services.Abstracts;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class CurrencyService : ContextAccess
    {
        private ValidationContext validationContext { get; set; }

        public CurrencyService(SMDbContext context)
            : base(context)
        {
        }

        public CurrencyDTO Create(CreateCurrencyDTO dto)
        {
            this.validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, this.validationContext);

            Currency toCreate = dto.Adapt<Currency>();

            this.context.Add(toCreate);
            this.context.SaveChanges();

            return toCreate.Adapt<CurrencyDTO>();
        }

        public CurrencyDTO Get(int id)
        {
            Currency received = this.context.Currencies.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Currency));

            return received.Adapt<CurrencyDTO>();
        }

        public IEnumerable<CurrencyDTO> GetAll()
        {
            return this.context.Currencies
                .AsNoTracking()
                .ProjectToType<CurrencyDTO>();
        }

        public void Delete(int id)
        {
            Currency toDelete = this.context.Currencies.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Currency));
            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }

        public CurrencyDTO Update(int id, CurrencyUpdateDTO dto)
        {
            this.validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, this.validationContext);

            Currency toUpdate = this.context.Currencies.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Currency));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<CurrencyDTO>();
        }
    }
}
