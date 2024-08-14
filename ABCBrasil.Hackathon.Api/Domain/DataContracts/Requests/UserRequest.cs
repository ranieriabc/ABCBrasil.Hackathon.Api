using System.ComponentModel.DataAnnotations;

namespace ABCBrasil.Hackathon.Api.Domain.DataContracts.Requests
{
    public class UserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
