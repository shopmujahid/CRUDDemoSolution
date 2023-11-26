using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Data;


namespace ServiceContracts.DTO
{
    public class PersonResponse
    {
       
        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? Address { get; set; }
        public bool? RecieveNewsLetters { get; set; }
        public double? Age { get; set; }

        /// <summary>
        /// Compares current object with parameters object
        /// </summary>
        /// <param name="obj">The Person object to compare</param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if(obj.GetType() != typeof(PersonResponse)) return false;
            PersonResponse person = (PersonResponse)obj;
            return PersonId==person.PersonId && PersonName==person.PersonName && Email==person.Email && DateOfBirth==person.DateOfBirth && Gender==person.Gender 
                && CountryId==person.CountryId && Address==person.Address && RecieveNewsLetters==person.RecieveNewsLetters && CountryName==person.CountryName;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Person ID: {PersonId}, Person Name: {PersonName}, Email: {Email}, Date of Birth: {DateOfBirth?.ToString("dd MM yyyy")}, Gender: {Gender?.ToString()}, Country Id: {CountryId}, Country Name: {CountryName}, Address: {Address}, News Letter: {RecieveNewsLetters}, Age: {Age}";
        }
    }

    public static class PersonExtensions
    {
        /// <summary>
        /// An Extension method to convert Person object to Person Response object
        /// </summary>
        /// <param name="person">The Person object to convert</param>
        /// <returns></returns>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonId = person.PersonId,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                RecieveNewsLetters = person.RecieveNewsLetters,
                Address = person.Address,
                CountryId = person.CountryId,
                Gender = person.Gender,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null,
            };
        }

    }
}
