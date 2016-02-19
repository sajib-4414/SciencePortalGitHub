using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CustomAuthentication2.Models
{
    public class Comment
    {
        public string ID { get; set; }

        [ForeignKey("Post")]
        public string PostId { get; set; }

        public string Fullname { get; set; }
        [Required]
        public string CommentDescription { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CommentTime { get; set; }
        //navigation property
        
        public virtual Post Post { get; set; }
    }
}