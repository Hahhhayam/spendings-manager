﻿using BLL.Misc.DTO.Debt;
using BLL.Misc.DTO.Person;
using BLL.Misc.Exceptions;
using DAL.Context;
using DAL.Entities;
using DAL.QueryExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ContactService
    {
        private readonly SpendingsManagerDbContext context;

        public ContactService(
            SpendingsManagerDbContext context)
        {
            this.context = context;
        }

        public PersonDTO Create(CreatePersonDTO dto)
        {
            Person toAdd = dto.Adapt<Person>();

            this.context.Add(toAdd);
            this.context.SaveChanges();

            return toAdd.Adapt<PersonDTO>();
        }

        public PersonDTO Get(int id)
        {
            Person received = this.context.People
                .AsNoTracking()
                .GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Person));

            return received.Adapt<PersonDTO>();
        }

        public IEnumerable<DebtIncludedDTO> GetDebts(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonDTO> GetAll()
        {
            return this.context.People
                .AsNoTracking()
                .ProjectToType<PersonDTO>();
        }

        public PersonDTO Update(int id, PersonUpdateDTO dto)
        {
            Person toUpdate = this.context.People.SingleOrDefault(e => e.Id == id)
                    ?? throw new EntityNotFoundException(typeof(Person));
            toUpdate.Adapt(dto);
            this.context.SaveChanges();

            return toUpdate.Adapt<PersonDTO>();
        }

        public void Delete(int id)
        {
            Person toDelete = this.context.People.GetById(id)
                    ?? throw new EntityNotFoundException(typeof(Person));

            this.context.Remove(toDelete);
            this.context.SaveChanges();
        }
    }
}
