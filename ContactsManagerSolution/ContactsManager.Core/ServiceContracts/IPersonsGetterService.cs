using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts
{
    public interface IPersonsGetterService
    {
        /// <summary>
        /// Return All Persons
        /// </summary>
        /// <returns> Return a list of objects of PersonResponse type </returns>
        Task<List<PersonResponse>> GetAllPersons();

        /// <summary>
        /// Return the person object based on given person id
        /// </summary>
        /// <returns> Return Matching Person Object</returns>
        Task<PersonResponse?> GetPersonByPersonID(Guid? personID);


        /// <summary>
        /// Returns all Person objects taht matches with the given search filed  and serach string
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString);

    }
}
