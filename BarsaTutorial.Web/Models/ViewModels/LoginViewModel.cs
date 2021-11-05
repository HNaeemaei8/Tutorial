using System.ComponentModel.DataAnnotations;

namespace BarsaTutorial.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
} 
