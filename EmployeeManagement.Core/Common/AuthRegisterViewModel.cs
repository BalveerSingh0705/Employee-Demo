using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Core.Common
{
    public class AuthRegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool Terms { get; set; }
    }


}

