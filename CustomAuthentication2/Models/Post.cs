using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CustomAuthentication2.Models
{
    public class Post
    {
        
        public string ID { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Description { get; set; }

        [ForeignKey("Account")]
        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        
        public DateTime? PostingDate { get; set; }

        [Required]
        public string Category { get; set; }

        public string image { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        
    }
}