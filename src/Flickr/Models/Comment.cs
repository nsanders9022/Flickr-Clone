using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flickr.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Description { get; set; }
        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
