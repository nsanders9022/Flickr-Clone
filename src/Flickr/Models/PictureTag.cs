using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flickr.Models
{
    [Table("PicturesTags")]
    public class PictureTag
    {
        [Key]
        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
