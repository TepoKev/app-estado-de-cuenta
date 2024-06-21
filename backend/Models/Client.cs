using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }

        public string? Phone { get; set; }
    }
}