using System.ComponentModel.DataAnnotations;


namespace WebApplication1.ViewModels
{
    public class LoginViewModel
    {
       // [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
       // [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remeber me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

    }
}