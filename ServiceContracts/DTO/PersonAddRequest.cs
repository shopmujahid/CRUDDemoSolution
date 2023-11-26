using Entities;
using ServiceContracts.Enums;
using System;
using System.ComponentModel.DataAnnotations;
namespace ServiceContracts.DTO
{
    public class PersonAddRequest
    {
        [Required(ErrorMessage ="Person Name can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email Name can't be blank")]
        [EmailAddress(ErrorMessage ="Please provide valid email address")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool? RecieveNewsLetters { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryId = CountryId,
                Address = Address,
                RecieveNewsLetters = RecieveNewsLetters
            };
        }
    }
}
