using System;
using System.ComponentModel.DataAnnotations;

namespace TimeSheetApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [StringLength (16, MinimumLength=4, ErrorMessage = "Password must be between 4 to 16 charcters")]
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}