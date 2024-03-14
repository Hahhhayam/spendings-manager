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
        private readonly ValidationContext validationContext;

        public CurrencyService(
            SMDbContext context,
            ValidationContext validationContext)
            : base(context)
        {
            this.validationContext = validationContext;
        }

        public CurrencyDTO Create(CreateCurrencyDTO dto)
        {
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
            List<CurrencyDTO> result = new ();
            foreach (var tag in this.context.Currencies.AsNoTracking().ToList())
            {
                result.Add(tag.Adapt<CurrencyDTO>());
            }

            return result;
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
            Validator.ValidateObject(dto, this.validationContext);

            Currency toUpdate = this.context.Currencies.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Currency));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<CurrencyDTO>();
        }
    }
}
