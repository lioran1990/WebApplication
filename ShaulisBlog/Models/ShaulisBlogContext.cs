using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShaulisBlog.Models
{
    public class ShaulisBlogContext : DbContext
    {
        public ShaulisBlogContext() : base("name=ShaulisBlogContext")
        {
        }
        public System.Data.Entity.DbSet<ShaulisBlog.Models.Fan> Fans { get; set; }

        public System.Data.Entity.DbSet<ShaulisBlog.Models.BlogPost> Posts { get; set; }

        public System.Data.Entity.DbSet<ShaulisBlog.Models.BlogComment> BlogComments { get; set; }

        public System.Data.Entity.DbSet<ShaulisBlog.Models.CommentToFan> CommentToFan { get; set; }
    }
}