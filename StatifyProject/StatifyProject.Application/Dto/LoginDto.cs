using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatifyProject.Application.Dto
{
    public record LoginDto
    (

            [Required(ErrorMessage = "The Email field is required.")]
            [EmailAddress(ErrorMessage = "The Email field must be a valid email address.")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the Email field must be between 3 and 50 characters.")]
            string Email,

            [Required(ErrorMessage = "The Password field is required.")]
            [StringLength(255, MinimumLength = 6, ErrorMessage = "The length of the Password field must be between 6 and 255 characters.")]
           string Password



        );
        




        

    
}
