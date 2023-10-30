using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public class PersonsRepository : IPersonRepository
    {
        private readonly ApplicationDBContext _db;
        public PersonsRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<Person> AddPerson(Person person)
        {
            _db.Persons.Add(person);
            await _db.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeletePersonByPersonID(Guid personID)
        {
            _db.Persons.RemoveRange(_db.Persons.Where(temp => temp.PersonID == personID));
            int rowDeleted = await _db.SaveChangesAsync();
            return rowDeleted > 0;
        }

        public async Task<List<Person>> GetAllPersons()
        {
            return await _db.Persons.Include("Country").ToListAsync();
        }

        public async Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
            return await _db.Persons.Include("Country")
                .Where(predicate).ToListAsync();
        }

        public async Task<Person?> GetPersonByPersonID(Guid? personID)
        {
            return await _db.Persons.Include("Country").FirstOrDefaultAsync(temp => temp.PersonID == personID);
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            Person? matchingPerson = await _db.Persons.FirstOrDefaultAsync(temp => temp.PersonID == person.PersonID);
            if (matchingPerson == null)
                return person;
            matchingPerson.PersonID = person.PersonID;
            matchingPerson.Email = person.Email;
            matchingPerson.DateOfBirth = person.DateOfBirth;
            matchingPerson.Gender = person.Gender;
            matchingPerson.CountryID = person.CountryID;
            matchingPerson.Address = person.Address;
            matchingPerson.ReceiveNewsLetters = person.ReceiveNewsLetters;
            return matchingPerson;
        }
    }
}
