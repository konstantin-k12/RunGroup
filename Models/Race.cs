using RunGroupWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RunGroupWebApp.Models
{
    public class Race
    {
        private const string _errorMessage = "Поле обязательно для заполнения.";
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = _errorMessage)]
        public string Title { get; set; }
        [Required(ErrorMessage = _errorMessage)]
        public string Description { get; set; }
        [Required(ErrorMessage = _errorMessage)]
        public string Image { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public RaceCategory RaceCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
