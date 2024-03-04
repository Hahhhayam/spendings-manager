using BLL.DTO.Tag;
using BLL.Exceptions;
using BLL.Services.Abstracts;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TagService : ContextAccess
    {
        public TagService(SMDbContext context) : base(context)
        {
        }

        public TagDTO Create(CreateTagDTO dto)
        {
            Tag toCreate = dto.Adapt<Tag>();
            _context.Add(toCreate);
            _context.SaveChanges();

            return toCreate.Adapt<TagDTO>();
        }

        public TagDTO Get(int id)
        {
            Tag received = _context.Tags.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Tag));

            return received.Adapt<TagDTO>();
        }

        public IList<TagDTO> GetAll()
        {
            List<TagDTO> result = new();
            foreach (var tag in _context.Tags.AsNoTracking().ToList())
            {
                result.Add(tag.Adapt<TagDTO>());
            }

            return result;
        }

        public void Delete(int id)
        {
            Tag toDelete = _context.Tags.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Tag));

            _context.Remove(toDelete);
            _context.SaveChanges();
        }

        public TagDTO Update(int id, TagUpdateDTO dto)
        {
            Tag toUpdate = _context.Tags.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Tag));

            toUpdate.Adapt(dto);
            _context.SaveChanges();

            return toUpdate.Adapt<TagDTO>();
        }
    }
}
