using BLL.DTO.MoneyFormat;
using BLL.Exceptions;
using BLL.Services.Abstracts;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class MoneyFormatService : ContextAccess
    {
        public MoneyFormatService(SMDbContext context) : base(context)
        {
        }

        public MoneyFormatDTO Create(CreateMoneyFormatDTO dto)
        {
            MoneyFormat toCreate = dto.Adapt<MoneyFormat>();
            _context.Add(toCreate);
            _context.SaveChanges();

            return toCreate.Adapt<MoneyFormatDTO>();
        }

        public MoneyFormatDTO Get(int id)
        {
            MoneyFormat received = _context.MoneyFormats.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));

            return received.Adapt<MoneyFormatDTO>();
        }

        public IList<MoneyFormatDTO> GetAll()
        {
            List<MoneyFormatDTO> result = new();
            foreach (var tag in _context.Tags.AsNoTracking().ToList())
            {
                result.Add(tag.Adapt<MoneyFormatDTO>());
            }

            return result;
        }

        public void Delete(int id)
        {
            MoneyFormat toDelete = _context.MoneyFormats.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));

            _context.Remove(toDelete);
            _context.SaveChanges();
        }

        public MoneyFormatDTO Update(int id, MoneyFormatUpdateDTO dto)
        {
            MoneyFormat toUpdate = _context.MoneyFormats.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));

            toUpdate.Adapt(dto);
            _context.SaveChanges();

            return toUpdate.Adapt<MoneyFormatDTO>();
        }
    }
}
