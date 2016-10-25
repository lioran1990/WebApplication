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
            try
            {
                IQueryable<BlogPost> posts = (from c in db.Posts orderby c._releaseDate descending select c).Take(1);
                if (posts.Any())
                {
                    first.Add(posts.First());
                    return View(first);
                }
                else
                    return View();
            }
            catch (Exception)
            {

                return View();
            }
           
        }

        
        [HttpPost]
        public ActionResult FilterPosts(int minComments, DateTime fromDate, DateTime untilDate, string postTitle = "", string wordsInComments = "", string commentWriter = "")
        {
            IEnumerable<BlogPost> filteredPosts = db.Posts; // Holds the result set

            // If both "Title" and "Comments contain" were given as filter
            if ((postTitle != string.Empty) && (wordsInComments != string.Empty))
            {
                filteredPosts = from p in db.Posts
                                join c in db.BlogComments on p.ID equals c.PostId
                                where p.Comments.Count() >= minComments &&
                                p._releaseDate >= fromDate &&
                                p._releaseDate <= untilDate &&
                                p._title.ToLower().Contains(postTitle.ToLower()) &&
                                c._text.Contains(wordsInComments)
                                select p;
            }

            // If "Comments contain" was given as filter and "Title" field was blank
            if ((postTitle == string.Empty) && (wordsInComments != string.Empty))
            {
                filteredPosts = from p in db.Posts
                                join c in db.BlogComments on p.ID equals c.PostId
                                where p.Comments.Count() >= minComments &&
                                p._releaseDate >= fromDate &&
                                p._releaseDate <= untilDate &&
                                c._text.Contains(wordsInComments)
                                select p;
            }

            // If "Title" was given as filter and "Comments contain" field was blank
            if ((postTitle != string.Empty) && (wordsInComments == string.Empty))
            {
                filteredPosts = from p in db.Posts
                                where p.Comments.Count() >= minComments &&
                                p._releaseDate >= fromDate &&
                                p._releaseDate <= untilDate &&
                                p._title.ToLower().Contains(postTitle.ToLower())
                                select p;
            }

            // If "Comment Writer" was given as filter and "Title", "Comments contain" were NOT given as filter
            if ((commentWriter != string.Empty) && (postTitle == string.Empty) && (wordsInComments == string.Empty))
            {
                filteredPosts = from p in db.Posts
                                join c in db.BlogComments on p.ID equals c.PostId
                                where c._author.ToUpper().Contains(commentWriter.ToUpper()) &&
                                p._releaseDate >= fromDate &&
                                p._releaseDate <= untilDate
                                select p;
            }

            // If none of "Title", "Comments contain", "Comment Writer" were given as filter
            if ((commentWriter == string.Empty) && (postTitle == string.Empty) && (wordsInComments == string.Empty))
            {
                filteredPosts = from p in db.Posts
                                where p.Comments.Count() >= minComments &&
                                p._releaseDate >= fromDate &&
                                p._releaseDate <= untilDate
                                select p;
            }

            // Make sure to return list with distinct values to avoid duplicate posts in the view
            return View("Index", filteredPosts.ToList().Distinct());
        }



        //
        // POST: /Blog/FilterComments
        /*
        * Method which handles comments searches from ManageComments view
        */
        [HttpPost]
        public ActionResult FilterComments(string writer, string contains, int PostId)
        {
            BlogPost post = db.Posts.Find(PostId);  // Holds the relevant post, according to PostId

            IEnumerable<BlogComment> filteredComments = post.Comments; // Holds all of its comments

            // If "Writer" was given as input parameter
            if (!String.IsNullOrWhiteSpace(writer))
                filteredComments = filteredComments.Where(f => f._author.ToLower().Contains(writer.ToLower()));

            // If "Contains" was given as input parameter
            if (!String.IsNullOrWhiteSpace(contains))
                filteredComments = filteredComments.Where(f => f._text.ToLower().Contains(contains.ToLower()));

            // Update ViewBag.commentList for "_CommentList" partial view
            ViewBag.commentList = filteredComments.ToList();

            // Return "_CommentList" partial view
            return PartialView("_CommentList");

        }

        public ActionResult postComment(int postid, string title, string authorname, string _websiteOfAuthor, string _text)
        {
            var comment = new BlogComment()
            {
                PostId = postid,
                _title = " ",
                _author = authorname,
                _websiteOfAuthor = _websiteOfAuthor,
                _text = _text,
                CommentDate = DateTime.Now
        };

            db.BlogComments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}