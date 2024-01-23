using System.ComponentModel.DataAnnotations;

namespace Utilities.Models
{
    public class WebTableDetails
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public long Salary { get; set; }
        public string Department { get; set; } = string.Empty;
    }
}