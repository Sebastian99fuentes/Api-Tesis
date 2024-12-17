using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Comments;
using api.Models;
using Npgsql.Replication;

namespace api.Controllers.Mappers
{
    public static class CommetMappers
    {
        public static CommentDto ToCommentDto(this Comments commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                
                Comentario = commentModel.Comentario,
                
                CreatedOn = commentModel.CreatedOn,
                //FK
                AreaId = commentModel.AreaId,

                ImplementoId = commentModel.ImplementoId

                

            };
        }

// Transforma el DTO (CreateUpdateComentRequestDto) en una entidad Comments, que es lo que 
// probablemente necesitas para guardarlo o actualizarlo en la base de datos.
// Usa el id del área (o comentario) que se pasó como parámetro desde la ruta.

         public static Comments ToCommentFromCreate(this CreateUpdateComentRequestDto commentModel, Guid id, bool isArea)
         {
             // Inicializamos el comentario con las propiedades comunes
             var comment = new Comments
                 {
                       Comentario = commentModel.Comentario,
                  };

               // Asignamos el ID de área o implemento según corresponda
               if (isArea)
               {
                      comment.AreaId = id;
               }
              else
              {
                    comment.ImplementoId = id;
              }

                   return comment;   
         }

    }
} 


