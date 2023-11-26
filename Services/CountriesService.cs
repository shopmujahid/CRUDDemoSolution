using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountryServices
    {
        private readonly List<Country> _countries;
        public CountriesService()
        {
            _countries = new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryRequest)
        {
            // Validation: CountryAddRequest can't be null
            if (countryRequest==null)
            {
                throw new ArgumentNullException(nameof(countryRequest));
            }

            // Validation: Country name in CountryAddRequest can't be null
            if (countryRequest.CountryName==null) 
            { 
                throw new ArgumentException(nameof(countryRequest.CountryName));
            }

            // Validation: Country name in CountryAddRequest Can't be duplicate
            if (_countries.Where(temp => temp.CountryName == countryRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given Country name " + countryRequest.CountryName + " already exist");   
            }

            Country country = countryRequest.ToCountry();

            // Gnerate Country Id
            country.CountryId = Guid.NewGuid();
            _countries.Add(country);
            return country.ToCountryResponse();
        }

        public CountryResponse? GetCountrybyId(Guid? countryId)
        {
            if (countryId == null)
            {
                return null;
            }
            Country? country = _countries.FirstOrDefault(temp => temp.CountryId == countryId);

            if (country == null)
                return null;


            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetCountryList()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }
    }
}