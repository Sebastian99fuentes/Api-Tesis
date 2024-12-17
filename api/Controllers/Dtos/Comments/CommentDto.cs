using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.Comments
{
    public class CommentDto
    {
        public Guid  Id  {get; set; }
        public string Comentario {get; set;}= string.Empty;
        public DateTime CreatedOn {get; set;} = DateTime.Now;
        
        // Navigation 
        //FK
        public Guid? AreaId {get; set; }     // Navigation 


        public Guid? ImplementoId {get; set; }     // Navigation 
    }
}