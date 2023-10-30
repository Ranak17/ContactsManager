using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    /// <summary>
    /// Represent the DTO Class that contains the person details to update
    /// </summary>
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage = "Person ID Can't be blank")]
        public Guid PersonID { get; set; }

        [Required(ErrorMessage = "Person NAme can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email Can't be blank")]
        [EmailAddress(ErrorMessage = "Email value should be valid Email")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderEnum? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetter { get; set; }

        /// <summary>
        /// Convert the new object of PersonAddRequest into a new object of Person type
        /// </summary>
        /// <returns>Persons Object</returns>
        public Person ToPerson()
        {
            return new Person
            {
                PersonID = PersonID,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetter
            };
        }

    }
}
