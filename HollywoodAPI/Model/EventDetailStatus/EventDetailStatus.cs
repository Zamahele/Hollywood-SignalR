using System.Collections.Generic;

namespace HollywoodAPI.Model.EventDetailStatus
{
    public class EventDetailStatus
    {

        public int EventDetailStatusId { get; set; }
        public string EventDetailStatusName { get; set; }   
        

        public List<string> EventDetailStatusNames()
        {
            return new List<string>{ "Active", "Scratched", "Closed" };
        }
    }
}
