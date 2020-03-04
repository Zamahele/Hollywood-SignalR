using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.EventDetailStatus
{
    public class EventDetailStatus
    {
        [Key]
        [DisplayName("Status Id")]
        public int EventDetailStatusId { get; set; }
        [DisplayName("Status")]
        [Required]
        public string EventDetailStatusName { get; set; }   
        
        public List<string> EventDetailStatusNames()
        {
            return new List<string>{ "Active", "Scratched", "Closed" };
        }
    }
}
