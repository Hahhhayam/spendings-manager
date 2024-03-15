using System.ComponentModel.DataAnnotations;
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
        private ValidationContext validationContext { get; set; }

        public MoneyFormatService(SMDbContext context)
            : base(context)
        {
        }

        public MoneyFormatDTO Create(CreateMoneyFormatDTO dto)
        {
            this.validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, this.validationContext);

            MoneyFormat toCreate = dto.Adapt<MoneyFormat>();

            this.context.Add(toCreate);
            this.context.SaveChanges();

            return toCreate.Adapt<MoneyFormatDTO>();
        }

        public MoneyFormatDTO Get(int id)
        {
            MoneyFormat received = this.context.MoneyFormats.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));

            return received.Adapt<MoneyFormatDTO>();
        }

        public IList<MoneyFormatDTO> GetAll()
        {
            List<MoneyFormatDTO> result = new();
            foreach (var tag in this.context.Tags.AsNoTracking().ToList())
            {
                result.Add(tag.Adapt<MoneyFormatDTO>());
            }

            return result;
        }

        public void Delete(int id)
        {
            MoneyFormat toDelete = this.context.MoneyFormats.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));
            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }

        public MoneyFormatDTO Update(int id, MoneyFormatUpdateDTO dto)
        {
            this.validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, this.validationContext);

            MoneyFormat toUpdate = this.context.MoneyFormats.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(MoneyFormat));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<MoneyFormatDTO>();
        }
    }
}
