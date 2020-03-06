using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace SignalRDbUpdates.Controllers
{

    [AuthenticationAccess(Roles = "Admin")]
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        /// 
        /// Injecting Role Manager
        /// 
        /// 
        //public RoleController(ApplicationRoleManager roleManager)
        //{
        //    this._roleManager = roleManager;
        //}
        private ApplicationRoleManager RoleManager
        {
            get => this._roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            set => this._roleManager = value;
        }

        public ActionResult Index()
        {
            var roles = RoleManager.Roles.ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<ActionResult> Create(IdentityRole role)
        {
            await RoleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}