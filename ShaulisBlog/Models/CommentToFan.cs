using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShaulisBlog.Models
{
    public class CommentToFan
    {
        public int ID { get; set; }
        public BlogComment comment { get; set; }
        public Fan fan { get; set; }
    }
}