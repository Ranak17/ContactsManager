using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts
{
    /// <summary>
    /// Representing  business logic for manipulating Country Entity
    /// </summary>
    public interface ICountriesGetterService
    {

        /// <summary>
        /// Return all countries from the list
        /// </summary>
        /// <returns></returns>
        Task<List<CountryResponse>> GetAllCountries();

        /// <summary>
        /// Return a country response object based on given ocuntry id
        /// </summary>
        /// <param name="countryID"></param>
        /// <returns></returns>
        Task<CountryResponse?> GetCountryByCountryID(Guid? countryID);


    }
}