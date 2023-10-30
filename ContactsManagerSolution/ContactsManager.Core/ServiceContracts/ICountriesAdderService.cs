using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts
{
    /// <summary>
    /// Representing  business logic for manipulating Country Entity
    /// </summary>
    public interface ICountriesAdderService
    {
        /// <summary>
        /// Add a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest"></param>
        /// <returns></returns>
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);

    }
}