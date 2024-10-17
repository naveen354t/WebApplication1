using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage ="Student name is required")]
        [StringLength(30)]
        public string StudentName { get; set; }
        [EmailAddress(ErrorMessage ="please enter a valid email address")]
        public string Email { get; set; }

        //[Range(10,20)]
        //public int Age { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public String DOB { get; set; }

        //public string Password { get; set; }
        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }

    }
}
