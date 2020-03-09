using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EventDetail
{
    public class EventDetail
    {
        //[Key]
        [Required]
        [DisplayName("Event Detail Id#")]
        public int EventDetailId { get; set; }


        //[ForeignKey("EventId")]
        [DisplayName("Event")]
        public int EventId { get; set; }
        [DisplayName("Event")]
        public Event.Event Event { get; set; }

        [Required]
        //[ForeignKey("EventDetailStatusId")]
        [DisplayName("Status")]
        public int EventDetailStatusId { get; set; }
        [DisplayName("Status")]
        public EventDetailStatus.EventDetailStatus EventDetailStatus { get; set; }

        [Required]
        [DisplayName("Event Detail Name")]
        public string EventDetailName { get; set; }
        [Required]
        [DisplayName("Event Detail Number")]
        public int EventDetailNumber { get; set; }

        [Required(ErrorMessage = "Please enter three digits  after a comma")]
        [DisplayName("Event Detail Odd")]
        public decimal EventDetailOdd { get; set; }


        [DisplayName("Finishing Position")]
        public int? FinishingPosition { get; set; }
        [Required]
        [DisplayName("First Timer")]
        public int FirstTimer { get; set; }
    }
}
