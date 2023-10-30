using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;

namespace Services
{
    public class PersonsDeleterService : IPersonsDeleterService
    {

        private readonly IPersonRepository _personRepository;

        public PersonsDeleterService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null) throw new ArgumentNullException(nameof(personID));
            Person? person = await _personRepository.GetPersonByPersonID(personID.Value);
            if (person == null) return false;
            await _personRepository.DeletePersonByPersonID(personID.Value);
            return true;
        }

    }
}
