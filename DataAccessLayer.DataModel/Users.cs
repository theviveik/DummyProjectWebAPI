using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DataModel
{
    public class Users
    {
        private string userName = string.Empty;
        private string email = string.Empty;
        private string userType = string.Empty;
        private string password = string.Empty;
        private string firstName = string.Empty;

        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "{0} is a mandatory field")]
        [MaxLength(100, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string UserName { get => userName; set => userName = value; }

        [Required(ErrorMessage = "{0} is a mandatory field")]
        [MaxLength(100, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string Email { get => email; set => email = value; }

        [Required(ErrorMessage = "{0} is a mandatory field")]
        [MaxLength(10, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string UserType { get => userType; set => userType = value; }

        [Required(ErrorMessage = "{0} is a mandatory field")]
        public string Password { get => password; set => password = value; }

        [Required(ErrorMessage = "{0} is a mandatory field")]
        [MaxLength(100, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string FirstName { get => firstName; set => firstName = value; }

        [MaxLength(100, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string? LastName { get; set; }

        [MaxLength(1000, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string? Address { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string? Country { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string? State { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string? City { get; set; }

        [MaxLength(20, ErrorMessage = "The {0} can not have more than {1} characters")]
        public string? Zip { get; set; }
    }
}
