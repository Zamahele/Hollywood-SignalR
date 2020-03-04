using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Event
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        //[ForeignKey("TournamentId")]
        public int TournamentId { get; set; }
        public Tournament.Tournament Tournament { get; set; }
        [Required]
        [DisplayName("Event Name")]
        public string EventName { get; set; }
        [Required]
        [DisplayName("Event Number")]
        public int EventNumber { get; set; }
        [Required]
        [DisplayName("Event Date Time")]
        public DateTime EventDateTime { get; set; }

        [DisplayName("Event End Date Time")]
        public DateTime?  EventEndDateTime { get; set; }
        [Required]
        [DisplayName("AutoClose")]
        public int AutoClose { get; set; }
    }
}
