using Microsoft.AspNetCore.Mvc;
using WebUsingDapper.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUsingDapper.Models
{
    public class RegisterModel
    {
        
        public Guid UserID { get; set; }
        [StringLength(100,MinimumLength =3,ErrorMessage ="FirstName must be 4 characters")]
        [Required]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }=default;
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default;   
        public bool IsActive { get; set; }
    }
}
