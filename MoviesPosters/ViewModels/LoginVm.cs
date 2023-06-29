using System.ComponentModel.DataAnnotations;

namespace MoviesPosters.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string? UserName { get; set; }
        public bool RememberMe { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
