using Microsoft.AspNetCore.Identity;
using MyBlogProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogProject.Models
{
    public class BlogCommentModel
    {

        public int Id { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; }
        public string ModeratorId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Comment")]
        public string Body { get; set; }

        public DateTime CommentCreated { get; set; }
        public DateTime? CommentUpdate { get; set; }
        public DateTime? CommentModerated { get; set; }
        public DateTime? CommentDeleted { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Moderated Comment")]
        public string CommentModeratedBody { get; set; }

        public ModerationType ModerationType { get; set; }

        // Navigation Properties
        public virtual BlogPostModel Post { get; set; }
        public virtual IdentityUser Author { get; set; }
        public virtual IdentityUser Moderator { get; set; }
        


    }
}
