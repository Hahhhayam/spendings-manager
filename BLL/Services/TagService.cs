using System.ComponentModel.DataAnnotations;
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
        private ValidationContext validationContext { get; set; }

        public TagService(SMDbContext context)
            : base(context)
        {
        }

        public TagDTO Create(CreateTagDTO dto)
        {
            this.validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, this.validationContext);

            Tag toCreate = dto.Adapt<Tag>();
            this.context.Add(toCreate);
            this.context.SaveChanges();

            return toCreate.Adapt<TagDTO>();
        }

        public TagDTO Get(int id)
        {
            Tag received = this.context.Tags.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Tag));

            return received.Adapt<TagDTO>();
        }

        public IList<TagDTO> GetAll()
        {
            List<TagDTO> result = new ();
            foreach (var tag in this.context.Tags.AsNoTracking().ToList())
            {
                result.Add(tag.Adapt<TagDTO>());
            }

            return result;
        }

        public void Delete(int id)
        {
            Tag toDelete = this.context.Tags.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Tag));
            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }

        public TagDTO Update(int id, TagUpdateDTO dto)
        {
            this.validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, this.validationContext);

            Tag toUpdate = this.context.Tags.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Tag));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<TagDTO>();
        }
    }
}
