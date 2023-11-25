using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTest
{
    public class CountriesServiceTest
    {
        private readonly ICountryServices _countriesServices;
        public CountriesServiceTest()
        {
            _countriesServices = new CountriesService();
        }

        //When CountryAdd Request is null, it should throw Argument null exception
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest request = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _countriesServices.AddCountry(request);
            });
         }

        // When Country name is null 
        [Fact]
        public void AddCountry_EmptyCountry()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = null
            };

            //Assert
            Assert.Throws<ArgumentException>(() => 
            {
                //Act
                _countriesServices.AddCountry(countryAddRequest);
             });
        }

        // Country name can't be duplicate
        [Fact]
        public void AddCountry_DuplicateDountry()
        {
            //Arrange
            CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "India" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "India" };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesServices.AddCountry(countryAddRequest1);
                _countriesServices.AddCountry(countryAddRequest2);
            });
        }

        // Make sure country is added
        [Fact]
        public void AddCountry_ValidCountry()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Canada" };
            //Act
            CountryResponse countryResponse =  _countriesServices.AddCountry(countryAddRequest);
            List<CountryResponse> country_List = _countriesServices.GetCountryList();   
            //Assert
            Assert.True(countryResponse.CountryID != Guid.Empty);
            Assert.Contains(countryResponse, country_List);
        }

        //Get all Counties
        [Fact]
        public void ValidateAllCountries()
        {
            //Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>() {
        new CountryAddRequest() { CountryName = "USA" }
      };

            //Act
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();

            foreach (CountryAddRequest country_request in country_request_list)
            {
                countries_list_from_add_country.Add(_countriesServices.AddCountry(country_request));
            }

            List<CountryResponse> actualCountryResponseList = _countriesServices.GetCountryList();

            //read each element from countries_list_from_add_country
            foreach (CountryResponse expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, actualCountryResponseList);
            }

        }

        // Validate Empty list
        [Fact]
        public void Validate_Empty_List()
        {
           //Act
            List<CountryResponse> countryResponses = _countriesServices.GetCountryList();
           //Assert
           Assert.Empty(countryResponses);
        }
    }
}