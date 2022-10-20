using System.ComponentModel.DataAnnotations;

namespace CleanArcheMvc.API.Models
{
    public class RegisterModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The{0}must be at least {2} and at max " +
        "{1} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
