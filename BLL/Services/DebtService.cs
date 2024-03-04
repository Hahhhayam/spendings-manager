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
        public DebtService(SMDbContext context) : base(context)
        {
        }

        public DebtDTO Create(CreateDebtDTO dto)
        {
            Debt toCreate = dto.Adapt<Debt>();
            _context.Add(toCreate);
            _context.SaveChanges();

            return toCreate.Adapt<DebtDTO>();
        }

        public DebtDTO Get(int id)
        {
            Debt received = _context.Debt.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Debt));

            return received.Adapt<DebtDTO>();
        }

        public IList<DebtDTO> GetAll()
        {
            List<DebtDTO> result = new();
            foreach (var tag in _context.Debt.AsNoTracking().ToList())
            {
                result.Add(tag.Adapt<DebtDTO>());
            }

            return result;
        }

        public void Delete(int id)
        {
            Debt toDelete = _context.Debt.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Debt));

            _context.Remove(toDelete);
            _context.SaveChanges();
        }

        public DebtDTO Update(int id, DebtUpdateDTO dto)
        {
            Debt toUpdate = _context.Debt.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Debt));

            toUpdate.Adapt(dto);
            _context.SaveChanges();

            return toUpdate.Adapt<DebtDTO>();
        }
    }
}
