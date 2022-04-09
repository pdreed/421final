using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _421final.Models
{
    public class MyTeam
    {
        public int Id { get; set; }

        [Required]
        public int? TeamStyleId { get; set; }
        [ForeignKey("TeamStyleId")]
        public TeamStyle? TeamStyle { get; set; }
        public string? teamName { get; set; }
        public string? PG { get; set; }
        public string? SG { get; set; }
        public string? SF { get; set; }
        public string? PF { get; set; }
        public string? C { get; set; }
    }
}
