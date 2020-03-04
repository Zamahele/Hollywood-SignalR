using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Tournament
{
    public class Tournament
    {
        [Key]
        public int TournamentId { get; set; }
        [DisplayName("Tournament Name")]
        public string TournamentName { get; set; }
    }
}
