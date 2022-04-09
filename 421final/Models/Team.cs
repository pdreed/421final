using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _421final.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? name { get; set; }
        public string? conference { get; set; }
        public string? division { get; set; }
        public int? championshipsWon { get; set; }

        [DataType(DataType.Upload)]
        [DisplayName("Team Logo")]
        public byte[]? TeamLogo { get; set; }

    }
}
