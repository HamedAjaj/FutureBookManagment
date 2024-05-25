using System.ComponentModel.DataAnnotations;

namespace FutureOFTask.Dtos.Rating
{
    public class RatingCreateDTO
    {
        [Required]
        public string BookId { get; set; }

        [Required]
        [Range(1,5)]
        public int Score { get; set; }
    }
}
