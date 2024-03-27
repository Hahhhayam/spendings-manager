using BLL.Misc.DTO.Person;
using BLL.Misc.Exceptions;
using BLL.Services;
using BLL.Tests.Comparers;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace BLL.Tests
{
    [TestClass]
    public class ContactServicesTests
    {
        private ContactService contactService;
        private SpendingsManagerDbContext context;

        [TestInitialize]
        public void Setup()
        {
            context = new SpendingsManagerDbContext(
                    new DbContextOptionsBuilder<SpendingsManagerDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options);

            contactService = new ContactService(context);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void Create_CorrectData_Successfully(int testCasePair)
        {
            var (attached, expected) = TestPersonsCreation[testCasePair];

            var person = contactService.Create(attached);

            Assert.AreEqual(person, expected, new PersonDTOCreationEquality());
        }

        [TestMethod]
        public void Create_TwoEntities_IdPlacedCorrectly()
        {
            context.People.Add(new Person() { Name = "", Contact = "" });
            context.SaveChanges();
            var attached = new CreatePersonDTO()
            {
                Name = "EugenKPI",
                Contact = "0994506598",
            };

            var newPerson = contactService.Create(attached);

            Assert.AreEqual(2, newPerson.Id);
        }

        [TestMethod]
        public void GetAll_Successfully()
        {
            context.People.AddRange(
                new Person() { Id = 1, Name = "Example1", Contact = "https://mail.google.com/mail/u/0/?pli=1#inbox" },
                new Person() { Id = 2, Name = "Example1", Contact = "https://mail.google.com/mail/u/0/?pli=1#inbox" });
            context.SaveChanges();

            var acceptedPersons = contactService.GetAll();

            Assert.IsTrue(acceptedPersons.Count() == context.People.Count());
        }

        [TestMethod]
        public void GetById_CorrectId_Successfully()
        {
            context.People.Add(
              new Person() { Id = 502, Name = "Example1", Contact = "https://mail.google.com/mail/u/0/?pli=1#inbox" });
            context.SaveChanges();

            var acceptedPerson = this.contactService.Get(502);

            Assert.IsTrue(new PersonToDTOComparer().EqualsTo(this.context.People.Find(502), acceptedPerson));
        }

        [TestMethod]
        public void GetById_IncorrectId_ThrowNotFoundException()
        {
            context.People.Add(
              new Person() { Id = 502, Name = "Example1", Contact = "https://mail.google.com/mail/u/0/?pli=1#inbox" });
            context.SaveChanges();

            var act = () => this.contactService.Get(1);

            Assert.ThrowsException<EntityNotFoundException>(act);
        }

        // get correct DebtIncludedDTOs
        public void GetDebts_CorrectId_AllIncludes() 
        {
            context.AddRange(
                new Person() { Id = 2, Name = "Mom", Contact = "https://mail.google.com/mail/u/0/?pli=1#inbox" },
                new Transaction() { Id = 24, Name = "Cookies", Added = DateTime.UtcNow, });
            context.SaveChanges();


        }

        // get debts with invalid id

        // updated successfully

        // updated with invalid id

        // updated invalid dto

        // deleted

        // deleted with invalid id

        private readonly List<(CreatePersonDTO, PersonDTO)> TestPersonsCreation =
        [
            (new()
            {
                Name = "NameExample1",
                Contact = "emaiul-sder@awdfeva.abob",
            },
            new()
            {
                Id = 1,
                Name = "NameExample1",
                Contact = "emaiul-sder@awdfeva.abob",
            }),
            (new()
            {
                Name = "NameExample2",
                Contact = "+38(099)986-55-55",
            },
            new()
            {
                Id = 1,
                Name = "NameExample2",
                Contact = "+38(099)986-55-55",
            }),
            (new()
            {
                Name = "NameExample3",
                Contact = "https://t.me/roman_deni"
            },
            new()
            {
                Id = 1,
                Name = "NameExample3",
                Contact = "https://t.me/roman_deni"
            }),
        ];
    }
}