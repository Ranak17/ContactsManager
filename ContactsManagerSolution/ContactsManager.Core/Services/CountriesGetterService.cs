using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.DTO;

namespace Services
{
    public class CountriesGetterService : ICountriesGetterService
    {
        private readonly ICountriesRepository _countriesRepository;
        public CountriesGetterService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }
        public async Task<List<CountryResponse>> GetAllCountries()
        {
            
            return (await _countriesRepository.GetAllCountries()).Select(temp=>temp.ToCountryResponse()).ToList();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null) return null;
            Country? countryResponseFromList = await _countriesRepository.GetCountryByCountryID(countryID.Value);
            if (countryResponseFromList == null) return null;
            return countryResponseFromList.ToCountryResponse();
        }
    }
}