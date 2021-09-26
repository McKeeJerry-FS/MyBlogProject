using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogProject.Models
{
    public class BlogModel 
    {
        public int Id { get; set; } // Primary Key for the blog
        public string AuthorId { get; set; } // Blog Author, Foreign Key for this class becomes the Primary
                                             // for another

        [Required] // means that a name for the blog is required
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters.", MinimumLength = 2)] // This is a Data Annotation to specific the maximum length, an Error Message and a minimum character length
        public string BlogName { get; set; } // The name of the blog
        [Required] // means that a name for the blog is required
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters.", MinimumLength = 2)]
        public string Description { get; set; } // Description of twhat the blog is about

        [DataType(DataType.Date)]
        [Display(Name = "Created Date:")]
        public DateTime BlogCreated { get; set; } // The date and time the blog was created

        [DataType(DataType.Date)]
        [Display(Name = "Updated Date:")]
        public DateTime? BlogUpdated { get; set; } // The date and time the blog was updated
                                                   // "?" after the DateTime variable type means that the 
                                                   // data type can be nullable. The "?" is shorthand for 
                                                   // "Nullable<DateTime>"
        [Display(Name = "Blog Image")]
        public byte[] ImageData { get; set; } // This property is for storing data for an image
        [Display(Name = "Image Type")]
        public string ContentType { get; set; } // This is for storing what type of image is being stored
        

        [NotMapped]
        public IFormFile Image { get; set; } // ImageData and Content type both get there information from this 
                                             // variable
        /*
        This is an example of "Code First" Building of a Database. 
        This means designing the variables that will be used in a
        database
        */ 

        // Navigation Properties will be added later after building out the 
        // other models.

        public virtual IdentityUser Author { get; set; }
        public virtual ICollection<BlogPostModel> Posts { get; set; } = new HashSet<BlogPostModel>();
    }
}
