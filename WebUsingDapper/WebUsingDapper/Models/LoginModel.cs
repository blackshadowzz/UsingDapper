using System.ComponentModel.DataAnnotations;

namespace WebUsingDapper.Models
{
    public class LoginModel
    {
        public Guid UserID { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(100)]
        public string UserName { get; set; } = default;
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default;
        public bool RememberMe { get; set; }
    }
}
