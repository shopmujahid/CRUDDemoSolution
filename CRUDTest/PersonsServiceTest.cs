using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace CRUDTest
{
    public class PersonsServiceTest
    {
        //private fields
        private readonly IPersonService _personService;
        private readonly ICountryServices _countryServices;
        private readonly ITestOutputHelper _testOutputHelper;

        List<PersonResponse> personResponseList_from_Add = new List<PersonResponse>();  
        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonsService();
            _countryServices = new CountriesService();
            _testOutputHelper = testOutputHelper;
        }

        private void AddDummyPersons()
        {
            //Arrange
            CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Canada" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "India" };
            CountryAddRequest countryAddRequest3 = new CountryAddRequest() { CountryName = "China" };

            CountryResponse countryResponse1_from_Add = _countryServices.AddCountry(countryAddRequest1);
            CountryResponse countryResponse2_from_Add = _countryServices.AddCountry(countryAddRequest2);
            CountryResponse countryResponse3_from_Add = _countryServices.AddCountry(countryAddRequest3);

            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Mujahid",
                Address = "Canada",
                CountryId = countryResponse1_from_Add.CountryID,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1989-07-03"),
                Email = "Mujahid@email.com",
                RecieveNewsLetters = true
            };

            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "Sujahid",
                Address = "India",
                CountryId = countryResponse2_from_Add.CountryID,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1992-05-15"),
                Email = "Sujahid@email.com",
                RecieveNewsLetters = false
            };
            PersonAddRequest personAddRequest3 = new PersonAddRequest()
            {
                PersonName = "Nausheen",
                Address = "India",
                CountryId = countryResponse3_from_Add.CountryID,
                Gender = GenderOptions.Female,
                DateOfBirth = DateTime.Parse("1988-03-11"),
                Email = "Nausheen@email.com",
                RecieveNewsLetters = true
            };
            // Add Person to list

             List<PersonAddRequest> personAddRequestList_from_add = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2, personAddRequest3 };
            foreach (var request in personAddRequestList_from_add)
            {
                PersonResponse personResponse = _personService.AddPerson(request);
                personResponseList_from_Add.Add(personResponse);
            }
        }

        #region Add Person
        [Fact]
        public void AddPerson_NullPersonRequest()
        {
            // Arrange
            PersonAddRequest? personAddRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(personAddRequest);
            });
        }

        [Fact]
        public void AddPerson_NullPersonName()
        {
            //Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest() { PersonName= null};
            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.AddPerson(personAddRequest);
            });
        }

        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName ="Canada"};
            CountryResponse countryResponse = _countryServices.AddCountry(countryAddRequest);
            PersonAddRequest? personAddRequest = new PersonAddRequest() 
            { 
                PersonName = "Sujahid", 
                Email = "person@example.com", 
                Address = "sample address", 
                CountryId = countryResponse.CountryID, 
                Gender = GenderOptions.Male, 
                DateOfBirth = DateTime.Parse("2000-01-01"), 
                RecieveNewsLetters = true
            };

            //Act
            PersonResponse person_response_from_add = _personService.AddPerson(personAddRequest);
            _testOutputHelper.WriteLine("Person Added: " + person_response_from_add.ToString());
            List<PersonResponse> persons_list = _personService.GetAllPersons();

            //Assert
            Assert.True(person_response_from_add.PersonId != Guid.Empty);

            Assert.Contains(person_response_from_add, persons_list);
        }
        #endregion

        #region Get Person by PersonId
        //Null should be returned if person id is null
        [Fact]
        public void GetPerson_NullPersonId()
        {
            //Arrange
            Guid persionId = Guid.NewGuid();

            //Act
            PersonResponse? personResponse_from_GetPersonId = _personService.GetPersonByPersonId(persionId);

            //Assert
            Assert.Null(personResponse_from_GetPersonId);
        }

        //Should Retuen valid Person if Valid Person Id is provided
        [Fact]
        public void GetPerson_ValidPersonId() 
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Russia" };
            CountryResponse countryResponse_fromAdd = _countryServices.AddCountry(countryAddRequest);
            
            // Act
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName= "Mujahid",
                Email="sample@example.com",
                Address= "Sample Address",
                CountryId = countryResponse_fromAdd.CountryID,
                DateOfBirth = DateTime.Parse("1989-07-03"),
                Gender = GenderOptions.Male,
                RecieveNewsLetters = false 
            };
            PersonResponse personResponse_from_Add = _personService.AddPerson(personAddRequest);
            PersonResponse? personResponse_from_GetPersonById = _personService.GetPersonByPersonId(personResponse_from_Add.PersonId);

            //Assert
            Assert.Equal(personResponse_from_Add, personResponse_from_GetPersonById);
        }
        #endregion

        #region GetAllPersons
        // Get All Person should return empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            // Act
            List<PersonResponse> personResponses = _personService.GetAllPersons();

            // Assert
            Assert.Empty(personResponses);
        }

        // Get all persons
        [Fact]
        public void GetAllPersons_AfterAdding()
        {
            //Arrange
            AddDummyPersons();

            //Print Person list from Add
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person in personResponseList_from_Add)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            //Act
            List<PersonResponse> personResponseList_from_get = _personService.GetAllPersons();
            //Print Person list from GetAllPersons
            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse person in personResponseList_from_get)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            //Assert
            foreach (PersonResponse person_response_from_add in personResponseList_from_Add)
            {
                Assert.Contains(person_response_from_add, personResponseList_from_get);
            }
        }
        #endregion

        #region Get Filtered Persons
        // Return all Persons if Search by or Search text is empty
        [Fact]
        public void GetFilteredPerson_EmptySearch()
        {
            //Arrange
            AddDummyPersons();

            //Print Person list from Add
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person in personResponseList_from_Add)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            //Act
            List<PersonResponse>? personResponseList_from_get = _personService.GetFilteredPersons(nameof(Person.PersonName), "");
            //Print Person list from GetAllPersons
            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse person in personResponseList_from_get)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            //Assert
            foreach (PersonResponse person_response_from_add in personResponseList_from_Add)
            {
                Assert.Contains(person_response_from_add, personResponseList_from_get);
            }

        }

        [Fact]
        public void GetFilteredPerson_SearchByPersonName()
        {
            //Arrange
            AddDummyPersons();

            //Print Person list from Add
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person in personResponseList_from_Add)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            //Act
            List<PersonResponse>? personResponseList_from_get = _personService.GetFilteredPersons(nameof(Person.CountryId), "Canada");
            //Print Person list from GetAllPersons
            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse person in personResponseList_from_get)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            //Assert
            foreach (PersonResponse person_response_from_add in personResponseList_from_Add)
            {
                if (person_response_from_add.PersonName != null)
                {
                    if (person_response_from_add.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person_response_from_add, personResponseList_from_get);
                    }
                }
                
            }

        }
        #endregion

        #region Sorted Person
        // When we sort Person Name with descending order, it should return list of person in desc order by Person Name
        [Fact]
        public void GetSortedPersons()
        {
            //Arrange
            AddDummyPersons();

            //Print Person list from Add
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person in personResponseList_from_Add)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }
            List<PersonResponse> allPersons = _personService.GetAllPersons();
            //Act
            List<PersonResponse> personResponseList_from_sort = _personService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.ASC);
            
            //Print Person list from GetAllPersons
            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse person in personResponseList_from_sort)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            personResponseList_from_Add = personResponseList_from_Add.OrderBy(x => x.PersonName).ToList();

            //Assert
            for (int i = 0; i < personResponseList_from_Add.Count; i++)
            {
                Assert.Equal(personResponseList_from_Add[i], personResponseList_from_sort[i]);
            }
        }
        #endregion

        #region Update Person

        [Fact]
        public void UpdatePerson_NullRequest()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });
            
        }


        [Fact]
        public void UpdatePerson_InvalidPersonId()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest() { PersonId = Guid.NewGuid()};

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });

        }
        // When Person name is null, it should throw argument exception
        [Fact]
        public void UpdatePerson_NullPersonName()
        {
            // Arrange
            AddDummyPersons();
            PersonResponse personResponseToUpdate = personResponseList_from_Add.FirstOrDefault(c => c.PersonName=="Mujahid");
            PersonUpdateRequest personUpdateRequest = personResponseToUpdate.ToPersonUpdateRequest();

            // Act
            personUpdateRequest.PersonName = null;

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public void UpdatePerson_ValidDetails ()
        {
            AddDummyPersons();
            PersonResponse personResponseToUpdate = personResponseList_from_Add.FirstOrDefault(c => c.PersonName == "Mujahid");
            PersonUpdateRequest personUpdateRequest = personResponseToUpdate.ToPersonUpdateRequest();

            // Act
            personUpdateRequest.PersonName = "Updated Mujahid";
            personUpdateRequest.Email = "updatedemailmujahid@gmail.com";
            PersonResponse personResponse_fromUpdate = _personService.UpdatePerson(personUpdateRequest);
            PersonResponse personResponse_fromGet = _personService.GetPersonByPersonId(personResponse_fromUpdate.PersonId);

            //Assert
            Assert.Equal(personResponse_fromUpdate, personResponse_fromGet);
        }
        #endregion

        #region Delete Person
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            // Arrange
            bool is_deleted = _personService.DeletePerson(Guid.NewGuid());

            // Assert
            Assert.False(is_deleted);
        }

        [Fact]
        public void DeletePerson_ValidPersonId()
        {
            // Arrange
            AddDummyPersons();

            PersonResponse person_toDelete = personResponseList_from_Add.FirstOrDefault(x => x.PersonName == "Mujahid");

            // Act
            bool is_deleted = _personService.DeletePerson(person_toDelete.PersonId);

            // Assert
            Assert.True(is_deleted);
        }
        #endregion
    }
}
