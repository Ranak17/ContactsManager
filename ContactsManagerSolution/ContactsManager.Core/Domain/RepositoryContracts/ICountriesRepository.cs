using ContactsManager.Core.Domain.Entities;

namespace ContactsManager.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents Data access logic for managing person entity
    /// </summary>
    public interface ICountriesRepository
    {
        /// <summary>
        /// Add a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest"></param>
        /// <returns></returns>
        Task<Country> AddCountry(Country countryAddRequest);


        /// <summary>
        /// Return all countries from the list
        /// </summary>
        /// <returns></returns>
        Task<List<Country>> GetAllCountries();

        /// <summary>
        /// Return a country response object based on given country id
        /// </summary>
        /// <param name="countryID"></param>
        /// <returns></returns>
        Task<Country?> GetCountryByCountryID(Guid countryID);

        /// <summary>
        /// Returns a country object based on the given country name
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        Task<Country?> GetCountryByCountryName(string countryName);
    }
}