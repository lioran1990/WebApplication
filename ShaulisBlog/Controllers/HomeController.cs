using ShaulisBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShaulisBlog.Controllers
{
    public class HomeController : Controller
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();
        public ActionResult Index()
        {
            List<BlogPost> first = new List<BlogPost>();
            IQueryable<BlogPost> posts = (from c in db.Posts orderby c._releaseDate descending select c).Take(1);
            first.Add(posts.First());
            return View(first);
        }

       
        public ActionResult postComment(int postid, string title, string authorname, string website, string text)
        {
            var comment = new BlogComment()
            {
                PostId = postid,
                _title = title,
                _author = authorname,
                _websiteOfAuthor = website,
                _text = text
            };

            db.BlogComments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}