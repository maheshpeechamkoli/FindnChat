using System.ComponentModel.DataAnnotations;

namespace FindnChitChat.Dto
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must enter a vaild password")]
        public string Password { get; set; }
    }
}