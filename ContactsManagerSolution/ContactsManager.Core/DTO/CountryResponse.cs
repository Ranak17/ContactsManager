using ContactsManager.Core.Domain.Entities;

namespace ContactsManager.Core.DTO
{
    /// <summary>
    /// DTO Class that is used as return type for most of CountriesService Methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(CountryResponse)) return false;
            CountryResponse countryToCompare = (CountryResponse)obj;
            return CountryID == countryToCompare.CountryID && CountryName == countryToCompare.CountryName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class ContryExtesnions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse { CountryID = country.CountryID, CountryName = country.CountryName };
        }
    }
}
