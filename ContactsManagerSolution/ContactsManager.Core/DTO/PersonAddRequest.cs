using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    /// <summary>
    /// Act as DTO for inserting a new person
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Person NAme can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email Can't be blank")]
        [EmailAddress(ErrorMessage = "Email value should be valid Email")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderEnum? Gender { get; set; }
        [Required(ErrorMessage = "Please Select a country")]
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Convert the new object of PersonAddRequest into a new object of Person type
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            return new Person
            {
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetters
            };
        }
    }
}
