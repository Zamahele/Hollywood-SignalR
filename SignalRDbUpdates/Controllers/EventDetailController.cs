using System.Web.Mvc;
using SignalRDbUpdates.Models;

namespace SignalRDbUpdates.Controllers
{
    public class EventDetailController : Controller
    {
        // GET: EventDetail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMessages()
        {
            var messageRepository = new DataRepository();
             return PartialView("_Data", messageRepository.GetAllMessages());
            //return PartialView("_MessagesList", messageRepository.GetAllMessages());
        }
    }
}