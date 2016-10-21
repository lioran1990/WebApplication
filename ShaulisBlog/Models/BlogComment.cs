using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShaulisBlog.Models
{
    public class BlogComment
    {
        [Key]
        public int CommentID { get; set; }

        //Foreign key for Post
        [ForeignKey("BlogPost")]
        public int PostId { get; set; }
        [DisplayName("Comment Title")]
        public string _title { get; set; }

        [DisplayName("Comment Author")]
        public string _author { get; set; }
        [DisplayName("Author Website")]
        public string _websiteOfAuthor { get; set; }
        [DisplayName("Content")]
        public string _text { get; set; }

        public virtual BlogPost BlogPost { get; set; }

    }
}