using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _421final.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string? name { get; set; }

        [Required]
        public int? TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team? Team { get; set; }

        [Required]
        public int? PositionId { get; set; }
        [ForeignKey("PositionId")]
        public Position? Position { get; set; }
        public int? number { get; set; }
        public int? age { get; set; }
    }
}
