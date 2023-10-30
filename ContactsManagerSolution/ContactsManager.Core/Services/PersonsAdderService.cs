using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.DTO;
using Services.Helpers;

namespace Services
{
    public class PersonsAdderService : IPersonsAdderService
    {

        private readonly IPersonRepository _personRepository;

        public PersonsAdderService(IPersonRepository personRepository)
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
    }
}
