using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HollywoodAPI.Model.Event
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [ForeignKey("TournamentId")]
        public int TournamentId { get; set; }
        public Tournament.Tournament Tournament { get; set; }
        [Required]
        [DisplayName("Events Name")]
        public string EventName { get; set; }
        [Required]
        [DisplayName("Events Number")]
        public int EventNumber { get; set; }
        [Required]
        [DisplayName("Events Date Time")]
        public DateTime EventDateTime { get; set; }

        [DisplayName("Events End Date Time")]
        public DateTime?  EventEndDateTime { get; set; }
        [Required]
        [DisplayName("AutoClose")]
        public int AutoClose { get; set; }
    }
}
