using Microsoft.AspNetCore.Identity;

namespace MoviesPosters.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LasttName { get; set; }

        public List<Image>? Images { get; set; }
    }
}
