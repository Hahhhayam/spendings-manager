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
        public PersonService(SMDbContext context) : base(context)
        {
        }

        public PersonDTO Create(CreatePersonDTO dto)
        {
            Person toCreate = dto.Adapt<Person>();
            _context.Add(toCreate);
            _context.SaveChanges();

            return toCreate.Adapt<PersonDTO>();
        }

        public PersonDTO Get(int id)
        {
            Person received = _context.People.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Person));

            return received.Adapt<PersonDTO>();
        }

        public void Delete(int id)
        {
            Person toDelete = _context.People.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Person));

            _context.Remove(toDelete);
            _context.SaveChanges();
        }

        public PersonDTO Update(int id, PersonUpdateDTO dto)
        {
            Person toUpdate = _context.People.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Person));

            toUpdate.Adapt(dto);
            _context.SaveChanges();

            return toUpdate.Adapt<PersonDTO>();
        }
    }
}
