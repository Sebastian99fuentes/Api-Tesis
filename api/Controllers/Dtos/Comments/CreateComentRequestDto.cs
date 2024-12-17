using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.Comments
{
    public class CreateUpdateComentRequestDto
    {

        [Required]
        [MinLength(1, ErrorMessage ="Comment must be 1 characters")]
        [MaxLength(300, ErrorMessage ="Comment cannot be over 300 characters")]
        public string Comentario {get; set;}= string.Empty; 


    }
}