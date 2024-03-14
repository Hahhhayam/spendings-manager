using System.ComponentModel.DataAnnotations;
using BLL.DTO.Person;
using BLL.Exceptions;
using BLL.Services.Abstracts;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;

namespace BLL.Services
{
    public class PersonService : ContextAccess
    {
        private readonly ValidationContext validationContext;

        public PersonService(
            SMDbContext context,
            ValidationContext validationContext)
            : base(context)
        {
            this.validationContext = validationContext;
        }

        public PersonDTO Get(int id)
        {
            Person received = this.context.People.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Person));

            return received.Adapt<PersonDTO>();
        }

        public void Delete(int id)
        {
            Person toDelete = this.context.People.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Person));

            this.context.Remove(toDelete);
            context.SaveChanges();
        }

        public PersonDTO Update(int id, PersonUpdateDTO dto)
        {
            Person toUpdate = this.context.People.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Person));

            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<PersonDTO>();
        }
    }
}
