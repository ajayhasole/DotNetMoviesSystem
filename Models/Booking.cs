using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    public class Booking
    {
        [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-generated ID
        public int Id { get; set; }

        [Display(Name = "User ID")]
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        [Display(Name = "Movie ID")]
        [Required(ErrorMessage = "Movie ID is required")]
        public int MovieId { get; set; }

        [Display(Name = "Booking Date")]
        [Required(ErrorMessage = "Booking date is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Seat Number")]
        [Required(ErrorMessage = "Seat number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Seat number must be a positive number")]
        public int SeatNumber { get; set; }

        // Navigation property to Movie
        //[ForeignKey("MovieId")]
        //public virtual Movie Movie { get; set; }

        // Navigation property to User
        //[ForeignKey("UserId")]
        //public virtual User1 User1 { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie? MovieIdNavigation { get; set; } = null!;
        [ForeignKey("UserId")]
        public virtual User1? User1IdNavigation { get; set; } = null!;

    }
}
