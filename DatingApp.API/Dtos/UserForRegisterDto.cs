using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [StringLength (16, MinimumLength=4, ErrorMessage = "Password must be between 4 to 16 charcters")]
        public string Password { get; set; }
    }
}