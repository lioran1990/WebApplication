using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web;

namespace ShaulisBlog.Models
{
    public class BlogPost
    {
        public int ID { get; set; }

        [Required]
        [DisplayName("Post Title")]
        public string _title { get; set; }

        [Required]
        [DisplayName("Author Name")]
        public string _author { get; set; }

        [Required]
        [DisplayName("Author Website")]
        public string _websiteOfAuthor { get; set; }

        [Required]
        [DisplayName("Posted at")]
        public DateTime _releaseDate { get; set; }

        [Required]
        [DisplayName("Post's Text")]
        public string _text { get; set; }

        [DisplayName("Comments")]
        public virtual List<BlogComment> Comments { get; set; }

        [DisplayName("Image")]
        [Url]
        public string _image { get; set; }

        [DisplayName("Video")]
        [Url]
        public string _video { get; set; }

        /* [DisplayName("Post Image")]
         public HttpPostedFileBase _image { get; set; }
         [DisplayName("Post Video")]
         public HttpPostedFileBase _video { get; set; }*/

        public BlogPost()
        {
            _releaseDate = DateTime.Now;
        }
    }

}