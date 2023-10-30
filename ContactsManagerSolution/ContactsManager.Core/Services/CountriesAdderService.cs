using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.DTO;

namespace Services
{
    public class CountriesAdderService : ICountriesAdderService
    {
        private readonly ICountriesRepository _countriesRepository;
        public CountriesAdderService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            if (await _countriesRepository.GetCountryByCountryName(countryAddRequest.CountryName)!=null)
            {
                throw new ArgumentException("Duplicate country");
            }


            /*
                        bool isDuplicateCountry = false;
                        foreach (Country country in _countries)
                        {
                            if (country.CountryName == countryAddRequest.CountryName)
                            {
                                isDuplicateCountry = true;
                                break;
                            }
                        }
                        if (isDuplicateCountry)
                        {
                            throw new ArgumentException();
                        }*/

            Country countryName = countryAddRequest.ToCountry();
            countryName.CountryID = Guid.NewGuid();
            await _countriesRepository.AddCountry(countryName);
            return countryName.ToCountryResponse();
        }
    }
}