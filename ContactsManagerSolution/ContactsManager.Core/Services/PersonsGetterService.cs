using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.DTO;

namespace Services
{
    public class PersonsGetterService : IPersonsGetterService
    {

        private readonly IPersonRepository _personRepository;

        public PersonsGetterService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
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
