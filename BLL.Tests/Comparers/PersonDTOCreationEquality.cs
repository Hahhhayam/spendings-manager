using BLL.Misc.DTO.Person;

namespace BLL.Tests.Comparers
{
    internal class PersonDTOCreationEquality : IEqualityComparer<PersonDTO>
    {
        public bool Equals(PersonDTO? x, PersonDTO? y)
        {
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Contact == y.Contact;
        }

        public int GetHashCode(PersonDTO obj)
        {
            return base.GetHashCode();
        }
    }
}
