using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Comments;
using api.Controllers.Mappers;
using api.Interfaces;
using api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")] 
    [ApiController]
    public class CommentController : ControllerBase
    {
         private readonly ICommentRepository _commentRepository;
         private readonly IAreaRepository _IAreaRepository;

         private readonly IImplementosRepository _IImplementoRepository;
        public CommentController(  ICommentRepository commentRepository, IAreaRepository areaRepository, IImplementosRepository IImplementoRepository)
        {
            _commentRepository = commentRepository;
            _IAreaRepository = areaRepository;
            _IImplementoRepository =IImplementoRepository;
        } 

        [HttpGet ("GetAll-comments")]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            
            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);

        }

        [HttpGet("GetById-comments{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var comments = await _commentRepository.GetByIdAsync(id);
            
            if(comments == null)
            {
                return NotFound();
            }

            return Ok(comments.ToCommentDto());

        }

        [HttpPost("Create-comments{AoI:guid}")]
        public async Task<IActionResult> Create([FromRoute] Guid AoI, CreateUpdateComentRequestDto comenDto )
       {
            if (!ModelState.IsValid)
                 return BadRequest(ModelState);

               var areaExists = await _IAreaRepository.Exist(AoI);
                var implementoExists = await _IImplementoRepository.Exist(AoI);

                // Si el ID no existe en ambos repositorios, devolvemos un error
             if (!areaExists && !implementoExists)
                return BadRequest("El área o el implemento no existen");

              // Determinamos el tipo de comentario según si es área o implemento
              var isArea = areaExists;
              var commentModel = comenDto.ToCommentFromCreate(AoI, isArea);

             // Creamos el comentario en el repositorio
              await _commentRepository.CreateAsync(commentModel);

                 // Retornamos la respuesta de creación
              return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
  
        }

        [HttpPut]
        [Route("Update-comments{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateUpdateComentRequestDto ComenDto )
        {
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);


                var areaExists = await _IAreaRepository.Exist(id);
                var implementoExists = await _IImplementoRepository.Exist(id);
                var isArea = areaExists;
                // Si el ID no existe en ambos repositorios, devolvemos un error
             if (!areaExists && !implementoExists)
                return BadRequest("El área o el implemento no existen");
             
            var comment = await _commentRepository.UpdateAsync(id, ComenDto.ToCommentFromCreate(id,isArea));

            if(comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }


        [HttpDelete]
        [Route("Delete-comments{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        { 

            var commentModel = await _commentRepository.DeleteAsync(id);

            if(commentModel == null)
            {
                return NotFound("Comment does not exist");
            }

            return Ok();

        }

    }
}