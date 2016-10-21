using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShaulisBlog.Models;

namespace ShaulisBlog.Controllers
{
    public class FansClubController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();

        // GET: Fans
        /*public ActionResult Index()
        {
            return View(db.Fans.ToList());
        }*/
        // GET: Fans
        public ActionResult Index(string firstName, string lastName, string gender)
        {
            var GenderLst = new List<Gender>();
            var GenderQry = from f in db.Fans
                            orderby f._gender
                            select f._gender;

            GenderLst.AddRange(GenderQry.Distinct());
            ViewBag.gender = new SelectList(GenderLst);
            var fans = from fan in db.Fans
                       select fan;

            if (!String.IsNullOrEmpty(firstName))
            {
                fans = fans.Where(s => s._firstName.Contains(firstName));
            }
            if (!String.IsNullOrEmpty(lastName))
            {
                fans = fans.Where(s => s._lastName.Contains(lastName));
            }
            if (!string.IsNullOrEmpty(gender))
            {
                fans = fans.Where(x => x._gender.ToString() == gender);
            }
            return View(fans);
        }
        public ActionResult JoinCommentToFan()
        {
            IEnumerable<CommentToFan> query = from f in db.Fans.AsEnumerable()
                                              join c in db.BlogComments on (f._firstName + f._lastName) equals c._author
                                              select new CommentToFan
                                              {
                                                  comment = c,
                                                  fan = f,
                                              };
            return View(query);
        }
        // GET: Fans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // GET: Fans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FanID,_firstName,_lastName,_gender,_birthDate,_seniority")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Fans.Add(fan);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }

            return View(fan);
        }

        // GET: Fans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FanID,_firstName,_lastName,_gender,_birthDate,_seniority")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: Fans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fan fan = db.Fans.Find(id);
            db.Fans.Remove(fan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}