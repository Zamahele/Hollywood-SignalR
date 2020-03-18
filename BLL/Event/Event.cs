using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BLL.Validation;


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
        [CompareEventDates("EventEndDateTime", ErrorMessage = "Event Date must be <= Event End Date")]
        public DateTime EventDateTime { get; set; }

        [DisplayName("Event End Date Time")]
        [CompareEventDates("EventDateTime", ErrorMessage = "Event End Date must be >= Event Date")]
        public DateTime?  EventEndDateTime { get; set; }
        [Required]
        [DisplayName("AutoClose")]
        public int AutoClose { get; set; }
    }
}
