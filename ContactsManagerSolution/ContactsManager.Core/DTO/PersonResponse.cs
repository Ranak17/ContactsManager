using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Enums;

namespace ContactsManager.Core.DTO
{
    /// <summary>
    /// Represents DTO Class that is used as return type of most methods of Person service
    /// </summary>
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PersonResponse)) return false;
            PersonResponse person = (PersonResponse)obj;

            return PersonID == person.PersonID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {

            return new PersonUpdateRequest
            {
                PersonID = PersonID,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), Gender, true),
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetter = ReceiveNewsLetters
            };

        }

    }
    public static class PersoneExtension
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                CountryID = person.CountryID,
                Country = person.Country?.CountryName,
                Address = person.Address,
                Age = person.DateOfBirth != null ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
            };
        }
    }
}