using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SignalRDbUpdates.Models
{
    public class EventDetail
    {
        public int EventDetailId { get; set; }
        public int EventId { get; set; }
        public int EventDetailStatusId { get; set; }
        public string EventDetailName { get; set; }
        public int EventDetailNumber { get; set; }
        public decimal EventDetailOdd { get; set; }

        public int FinishingPosition { get; set; }
        public int FirstTimer { get; set; }
    }
}
