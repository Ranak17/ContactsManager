using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name Can't be blank")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Email Can't be blank")]
        [EmailAddress(ErrorMessage = "Email Should be in proper email address format")]
        [DataType(DataType.EmailAddress)]
        [Remote(action:"IsEmailAlreadyRegistered", controller:"Account", ErrorMessage = "Email is already in use")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain numbers only")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password Can;t be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Message Can't be blank")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password is dfferent from Password")]
        public string ConfirmPassword { get; set; }
    }
}
