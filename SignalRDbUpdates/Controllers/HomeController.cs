using System.Web.Mvc;
using SignalRDbUpdates.Models;

namespace SignalRDbUpdates.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetMessages()
        {
            var messageRepository = new MessagesRepository();
            return PartialView("_MessagesList", messageRepository.GetAllMessages());
            //return PartialView("_Data", messageRepository.GetAllMessages());
        }
      
    }
}