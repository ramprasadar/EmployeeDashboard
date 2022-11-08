using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Models
{
    public class userModel
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; }
    }
}
