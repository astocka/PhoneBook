using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class PersonModel
    {
        [Required]
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(32)]
        [RegularExpression(@"^[\D][\D]{2,}$", ErrorMessage = "Incorrect First Name.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(64)]
        [RegularExpression(@"^[\D][\D]{2,}$", ErrorMessage = "Incorrect Last Name.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(25)]
        [Phone] 
        [DisplayName("Phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }


        public PersonModel(int id, string firstName, string lastName, string phone, string email, DateTime created, DateTime? updated)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Created = created;
            Updated = updated;
        }

        public PersonModel()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}
