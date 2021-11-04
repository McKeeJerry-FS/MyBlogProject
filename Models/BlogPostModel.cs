using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyBlogProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

// This model is used to represent a "Blog Post"
// that has been created by an Authenticated User

namespace MyBlogProject.Models
{
    public class BlogPostModel
    {
        public int Id { get; set; }
       
        [Display(Name = "Blog Name")]
        public int BlogId { get; set; }
        public string BlogUserId { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {o} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {o} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Abstract { get; set; }

        [Required]
        public string PostContent { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date: ")]
        public DateTime PostCreated { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created Date: ")]
        public DateTime? PostUpdated { get; set; }

        
        // public bool IsReady { get; set; }
        public ReadyStatus ReadyStatus { get; set; }


        public string Slug { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
        
        [NotMapped]
        public IFormFile Image { get; set; }


        // Navigation Properties
        public virtual BlogModel Blog { get; set; }
        public virtual BlogUser BlogUser { get; set; }

        public virtual ICollection<BlogTagModel> Tags { get; set; } = new HashSet<BlogTagModel>();
        public virtual ICollection<BlogCommentModel> Comments { get; set; } = new HashSet<BlogCommentModel>();
        


    
    }
}
