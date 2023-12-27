using System.ComponentModel.DataAnnotations;

namespace pizza_mama_V2.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
