using ShaulisBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShaulisBlog.Controllers
{
    public class AddressesController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();

        // GET: Addresses
        public ActionResult Index()
        {
            return View(db.Fans.Select(x => x._address).ToList());
        }
    }
}