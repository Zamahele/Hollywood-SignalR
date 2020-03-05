using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HollywoodAPI.Model.EventDetail
{
    public class EventDetail
    {
        [Key]
        [Required]
        [DisplayName("Events Detail Id#")]
        public int EventDetailId { get; set; }


        [ForeignKey("EventId")]
        public int EventId { get; set; }
        public Event.Event Event { get; set; }

        [Required]
        [ForeignKey("EventDetailStatusId")]
        public int EventDetailStatusId { get; set; }
        public EventDetailStatus.EventDetailStatus EventDetailStatus { get; set; }

        [Required]
        [DisplayName("Events Detail Name")]
        public string EventDetailName { get; set; }
        [Required]
        [DisplayName("Events Detail Number")]
        public int EventDetailNumber { get; set; }

        [Required]
        [DisplayName("Events Detail Odd")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal EventDetailOdd { get; set; }

        [Required]
        [DisplayName("Finishing Position")]
        public int FinishingPosition { get; set; }
        [Required]
        [DisplayName("First Timer")]
        public int FirstTimer { get; set; }
    }
}
