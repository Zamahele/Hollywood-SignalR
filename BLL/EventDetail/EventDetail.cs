using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EventDetail
{
    public class EventDetail
    {

        //public int EventDetailId { get; set; }
        //public int EventId { get; set; }
        //public int EventDetailStatusId { get; set; }
        //public string EventDetailName { get; set; }
        //public int EventDetailNumber { get; set; }
        //public decimal EventDetailOdd { get; set; }

        //public int FinishingPosition { get; set; }
        //public int FirstTimer { get; set; }

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

        [Required]
        [DisplayName("Event Detail Odd")]
        //[Column(TypeName = "decimal(18.4)")]
        public decimal EventDetailOdd { get; set; }

        [Required]
        [DisplayName("Finishing Position")]
        public int FinishingPosition { get; set; }
        [Required]
        [DisplayName("First Timer")]
        public int FirstTimer { get; set; }
    }
}
