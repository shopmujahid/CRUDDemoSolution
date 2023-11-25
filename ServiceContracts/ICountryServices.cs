using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents Business logic for manipulating Country Entity
    /// </summary>
    public interface ICountryServices
    {
        /// <summary>
        /// Adds a Country object to a list of Countries
        /// </summary>
        /// <param name="countryRequest">Country object to add</param>
        /// <returns></returns>
        CountryResponse AddCountry(CountryAddRequest? countryRequest); 
        List<CountryResponse> GetCountryList();
    }
}