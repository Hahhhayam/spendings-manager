using BLL.Misc.DTO.Person;
using DAL.Entities;

namespace BLL.Tests.Comparers
{
    internal class PersonToDTOComparer : IComparer<Person, PersonDTO>
    {
        public bool EqualsTo(Person? x, PersonDTO? y)
        {
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Contact == y.Contact;
        }
    }
}
