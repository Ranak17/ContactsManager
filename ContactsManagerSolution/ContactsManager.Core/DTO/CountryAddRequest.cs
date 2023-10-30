using ContactsManager.Core.Domain.Entities;
namespace ContactsManager.Core.DTO
{
    public class CountryAddRequest
    {
        /// <summary>
        /// DTO Class for adding a new country
        /// </summary>
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country() { CountryName = CountryName };
        }
    }
}
