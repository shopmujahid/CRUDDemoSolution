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

        public List<PersonResponse>? GetFilteredPersons(string searchBy, string searchText);
        /// <summary>
        /// Retuens Sorted list of persons
        /// </summary>
        /// <param name="allPersons">Represents List of persons to be sorted</param>
        /// <param name="sortBy">Name of the Property based on which person should be sorted</param>
        /// <param name="sortOrder">ASC or DESC</param>
        /// <returns></returns>
        public List<PersonResponse>? GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
    }
}
