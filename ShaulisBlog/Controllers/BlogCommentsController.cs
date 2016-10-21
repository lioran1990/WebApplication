using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShaulisBlog.Models;
using System;
using System.Collections.Generic;

namespace ShaulisBlog.Controllers
{
    public class BlogCommentsController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();

        public ActionResult Index(int ?id,string commentTitle,string commentAuthor,string commentContent)
        {
            if (id.HasValue)
            {
                var blogComments = db.BlogComments.Include(c => c.BlogPost);
                
                if (!String.IsNullOrEmpty(commentTitle))
                {
                    blogComments = blogComments.Where(s => s._title.Contains(commentTitle));
                }
                if (!String.IsNullOrEmpty(commentAuthor))
                {
                    blogComments = blogComments.Where(s => s._author.Contains(commentAuthor));
                }
                if (!String.IsNullOrEmpty(commentContent))
                {
                    blogComments = blogComments.Where(s => s._text.Contains(commentContent));
                }
                return View(blogComments.ToList().Where(c => c.PostId == id));
            }
            else
            {
                
                var blogComments = db.BlogComments.Include(b => b.BlogPost);
                return View(blogComments.ToList());
            }

            
        }
        /*public ActionResult Index()
        {
            var blogComments = db.BlogComments.Include(b => b.BlogPost);
            return View(blogComments.ToList());
        }*/
        // GET: BlogComments/Details/5

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.BlogComments.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            return View(blogComment);
        }

        // GET: BlogComments/Create
        public ActionResult Create(int? id)
        {
            ViewBag.PostId = new SelectList(db.Posts, "ID", "_title");
            return View();
        }

        // POST: BlogComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,PostId,_title,_author,_websiteOfAuthor,_text")] BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
                db.BlogComments.Add(blogComment);
                db.SaveChanges();
                ViewBag.PostId = new SelectList(db.Posts, "ID", "_title", blogComment.PostId);
                return RedirectToAction("Index", new { id = blogComment.PostId });
            }

            ViewBag.PostId = new SelectList(db.Posts, "ID", "_title", blogComment.PostId);
            return View(blogComment);
        }

        // GET: BlogComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.BlogComments.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostId = new SelectList(db.Posts, "ID", "_title", blogComment.PostId);
            return View(blogComment);
        }

        // POST: BlogComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,PostId,_title,_author,_websiteOfAuthor,_text")] BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = blogComment.PostId });
            }
            ViewBag.PostId = new SelectList(db.Posts, "ID", "_title", blogComment.PostId);
            return View(blogComment);
        }

        // GET: BlogComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.BlogComments.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            return View(blogComment);
        }

        // POST: BlogComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogComment blogComment = db.BlogComments.Find(id);
            db.BlogComments.Remove(blogComment);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = blogComment.PostId });
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
