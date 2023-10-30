using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.DTO;
using Services.Helpers;

namespace Services
{
    public class PersonsUpdaterService : IPersonsUpdaterService
    {

        private readonly IPersonRepository _personRepository;

        public PersonsUpdaterService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }


        public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null) throw new ArgumentNullException(nameof(personAddRequest));

            //Model Validation
            ValidationHelper.ModelValidation(personAddRequest);

            //
            Person person = personAddRequest.ToPerson();
            person.PersonID = Guid.NewGuid();
            await _personRepository.AddPerson(person);
            //convert the Person object to personResponse Object
            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetAllPersons()
        {
            var persons = await _personRepository.GetAllPersons();
            return persons.Select(temp => temp.ToPersonResponse()).ToList();
        }

        public async Task<PersonResponse?> GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;
            Person? person = await _personRepository.GetPersonByPersonID(personID.Value);

            return person?.ToPersonResponse();
        }

        public async Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null) throw new ArgumentNullException(nameof(personUpdateRequest));

            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            //get matching person to update
            Person? matchingPerson = await _personRepository.GetPersonByPersonID(personUpdateRequest.PersonID);
            if (matchingPerson == null) throw new ArgumentException("Given person id doesn't exist");

            //update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetter;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            await _personRepository.UpdatePerson(matchingPerson);
            return matchingPerson.ToPersonResponse();

        }

        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null) throw new ArgumentNullException(nameof(personID));
            Person? person = await _personRepository.GetPersonByPersonID(personID.Value);
            if (person == null) return false;
            await _personRepository.DeletePersonByPersonID(personID.Value);
            return true;
        }

        public async Task<List<PersonResponse>> GetFilteredPersons(string? searchBy, string? searchString)
        {
            List<Person> persons = searchBy switch
            {
                nameof(PersonResponse.PersonName) =>
                     await _personRepository.GetFilteredPersons(temp =>
                    temp.PersonName.Contains(searchString)),
                nameof(PersonResponse.Email) =>
                                     await _personRepository.GetFilteredPersons(temp =>
                                    temp.Email.Contains(searchString)),
                nameof(PersonResponse.Gender) =>
                                     await _personRepository.GetFilteredPersons(temp =>
                                    temp.Gender.Contains(searchString)),
                nameof(PersonResponse.CountryID) =>
                                     await _personRepository.GetFilteredPersons(temp =>
                                    temp.Country.CountryName.Contains(searchString)),
                nameof(PersonResponse.Address) =>
                                     await _personRepository.GetFilteredPersons(temp =>
                                    temp.Address.Contains(searchString)),
                _ => await _personRepository.GetAllPersons()
            };
            return persons.Select(temp => temp.ToPersonResponse()).ToList();
        }
    }
}
