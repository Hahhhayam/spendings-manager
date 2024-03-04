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
        public CurrencyService(SMDbContext context) : base(context)
        {
        }

        public CurrencyDTO Create(CreateCurrencyDTO dto)
        {
            Currency toCreate = dto.Adapt<Currency>();
            _context.Add(toCreate);
            _context.SaveChanges();

            return toCreate.Adapt<CurrencyDTO>();
        }

        public CurrencyDTO Get(int id)
        {
            Currency received = _context.Currencies.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Currency));

            return received.Adapt<CurrencyDTO>();
        }

        public IList<CurrencyDTO> GetAll()
        {
            List<CurrencyDTO> result = new();
            foreach (var tag in _context.Currencies.AsNoTracking().ToList())
            {
                result.Add(tag.Adapt<CurrencyDTO>());
            }

            return result;
        }

        public void Delete(int id)
        {
            Currency toDelete = _context.Currencies.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Currency));

            _context.Remove(toDelete);
            _context.SaveChanges();
        }

        public CurrencyDTO Update(int id, CurrencyUpdateDTO dto)
        {
            Currency toUpdate = _context.Currencies.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Currency));

            toUpdate.Adapt(dto);
            _context.SaveChanges();

            return toUpdate.Adapt<CurrencyDTO>();
        }
    }
}
