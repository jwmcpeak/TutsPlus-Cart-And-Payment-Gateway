using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspNetStore.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Browse(string id)
        {
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            return View();
        }
    }
}