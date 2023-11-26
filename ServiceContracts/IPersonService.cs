using Entities;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;

namespace ServiceContracts
{
    /// <summary>
    /// Interface for manipulating Person entity
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Adds a new person to the list of persons
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns></returns>
        public PersonResponse AddPerson(PersonAddRequest personAddRequest);
        /// <summary>
        /// Returns a list of PersonResponse object
        /// </summary>
        /// <returns></returns>
        public List<PersonResponse> GetAllPersons();
        /// <summary>
        /// Returns Person response object based on person id
        /// </summary>
        /// <param name="persionId">Person id to search</param>
        /// <returns>Return matching person object</returns>
        public PersonResponse? GetPersonByPersonId(Guid persionId);
        /// <summary>
        /// Returns List of person after applying filter by person properties
        /// </summary>
        /// <param name="searchBy">Person property to apply filter on</param>
        /// <param name="searchText">Filter text</param>
        /// <returns>Returns List of person after applying filter by person properties</returns>
        public List<PersonResponse> GetFilteredPersons(string searchBy, string searchText);
        /// <summary>
        /// Retuens Sorted list of persons
        /// </summary>
        /// <param name="allPersons">Represents List of persons to be sorted</param>
        /// <param name="sortBy">Name of the Property based on which person should be sorted</param>
        /// <param name="sortOrder">ASC or DESC</param>
        /// <returns></returns>
        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
        /// <summary>
        /// Updates the Person details based on the given person id
        /// </summary>
        /// <param name="personUpdateRequest">Person Details to Update</param>
        /// <returns>Person object after updation</returns>
        public PersonResponse UpdatePerson(PersonUpdateRequest personUpdateRequest);
        /// <summary>
        /// Deletes a person based on the given person id
        /// </summary>
        /// <param name="personId">PersonID to delete</param>
        /// <returns>Returns true, if the deletion is successful; otherwise false</returns>
        public bool DeletePerson(Guid? personId);
    }
}
