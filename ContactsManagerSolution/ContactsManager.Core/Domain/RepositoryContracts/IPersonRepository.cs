using ContactsManager.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ContactsManager.Core.Domain.RepositoryContracts
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Adds a person object to the data source
        /// </summary>
        /// <param name="person"></param>
        /// <returns>returna person object after adding it to the table</returns>
        Task<Person> AddPerson(Person person);

        /// <summary>
        /// Return all person object based on the given person id
        /// </summary>
        /// <returns></returns>
        Task<List<Person>> GetAllPersons();

        Task<Person?> GetPersonByPersonID(Guid? personID);

        /// <summary>
        /// Return all person objects based on the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>All matching person with given  conditions </returns>
        Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate);

        /// <summary>
        /// Deletes a person object based on the person id
        /// </summary>
        /// <param name="personID"></param>
        /// <returns> return true if the deletion is successful, otherwise false</returns>
        Task<bool> DeletePersonByPersonID(Guid personID);

        /// <summary>
        /// Updates a person object (person name and other details)
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Task<Person> UpdatePerson(Person person);

    }
}