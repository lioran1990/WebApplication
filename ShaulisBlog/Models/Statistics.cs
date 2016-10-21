using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShaulisBlog.Models
{
    public class Statistics
    {
        private ShaulisBlogContext db = new ShaulisBlogContext();

        public void GenerateStatisticFiles()
        {
            //How many posts were added per day?
            var postsPerDay = db.Posts.GroupBy
                (x => new { x._releaseDate.Year, x._releaseDate.Month, x._releaseDate.Day })
                    .Select(g => new { Key = g.Key, Count = g.Count() })
                    .OrderBy(x => x.Key.Year).ThenBy(x => x.Key.Month).ThenBy(x => x.Key.Day);
            string path = System.Web.HttpContext.Current.Server.MapPath("~/ShowPostsPerDayResult.tsv");

            //write to file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, false))
            {
                string line = string.Format("{0}\t{1}", "Item", "Count"); //do headline
                file.WriteLine(line);

                foreach (var data in postsPerDay)
                {
                    line = string.Format("{0}/{1}/{2}\t{3}",data.Key.Day ,data.Key.Month, data.Key.Year, data.Count);
                    file.WriteLine(line);
                }
            }

            //How many posts do we have for postAuthor?
            var postsAuthorCount = db.Posts.GroupBy(x => x._author).Select(g => new { Key = g.Key, Count = g.Count() }).OrderBy(x => x.Key);
            path = System.Web.HttpContext.Current.Server.MapPath("~/ShowPostsAuthorResult.tsv");
            //write to file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, false))
            {
                string line = string.Format("{0}\t{1}", "Item", "Count"); //do headline
                file.WriteLine(line);

                foreach (var data in postsAuthorCount)
                {
                    line = string.Format("Author:{0}\t{1}", data.Key, data.Count);
                    file.WriteLine(line);
                }
            }
        }
    }
}