using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShaulisBlog.Models;
using System.Web;
using System;
using System.Collections.Generic;

namespace ShaulisBlog.Controllers
{
    public class BlogPostsController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();

        public ActionResult Index(string postTitle, string authorName)
        {
            var blogPosts = from post in db.Posts
                            select post;

            if (!String.IsNullOrEmpty(postTitle))
            {
                blogPosts = blogPosts.Where(s => s._title.Contains(postTitle));
            }
            if (!String.IsNullOrEmpty(authorName))
            {
                blogPosts = blogPosts.Where(s => s._author.Contains(authorName));
            }
            return View(blogPosts);
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            BlogPost blogPost = db.Posts.Find(id);
            
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
       
        public ActionResult Create()
        {

            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,_title,_author,_websiteOfAuthor,_releaseDate,_text,_image,_video")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost._releaseDate = DateTime.Now;
                db.Posts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }
        
        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "ID,_title,_author,_websiteOfAuthor,_releaseDate,_text,_image,_video")] BlogPost blogPost, HttpPostedFileBase image, HttpPostedFileBase video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.Posts.Find(id);
            db.Posts.Remove(blogPost);
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
