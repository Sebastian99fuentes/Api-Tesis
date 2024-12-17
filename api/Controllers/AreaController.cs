using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Area;
using api.Controllers.Helpers;
using api.Controllers.Mappers;
using api.Data;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/area")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        // private readonly ApplicationDBContext _context;
         private readonly IAreaRepository _areaRepository;

         private readonly IReservasRepository _IReservaRepository;
        public AreaController(  IAreaRepository areaRepository, IReservasRepository IReservaRepository)
        {
            _areaRepository = areaRepository;
            _IReservaRepository = IReservaRepository;
        }

      
        [HttpGet ("GetAll-areas")]
        // [Authorize]
        public async Task<IActionResult> GetAll( [FromQuery] QueryObject query)
        {
            // está recuperando una lista de todas las áreas desde la base de datos
            // (_context.Area.ToList()) y luego transforma cada objeto de tipo Area en su correspondiente DTO
            // (AreaDto) usando el método ToAreaDto(). El resultado final es una colección de objetos AreaDto. 

            var areas = await _areaRepository.GetAllAsync(query);

            var areasDto = areas.Select(a =>a.ToAreasDto());
                
            return Ok(areasDto);
        }

         [HttpGet("GetById-areas{id:guid}")]
         public async Task<IActionResult> GetById([FromRoute] Guid id)
         {
            var area = await _areaRepository.GetByIdAsync(id);

            if(area == null)
            {
                return NotFound();
            }
            
            return Ok(area);
         } 

        [HttpPost("create-area")]
         public  async Task<IActionResult> Create ([FromBody] CreateAreaRequestDto AreaDto)
         {
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
             

                var areaModel = AreaDto.ToAreaFromCreateDto();
                await _areaRepository.CreateAsync(areaModel);
                return CreatedAtAction(nameof(GetById), new{ id = areaModel.Id}, areaModel.ToAreaDto());
         }

         [HttpPut]
         [Route("Update-areas/{id:guid}")]
           public  async Task<IActionResult> Update ([FromRoute] Guid id, [FromBody] CreateAreaRequestDto Area)
         {

            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
             
               var areaModel = await _areaRepository.UpdateAsync(id,Area );

               if(areaModel == null)
               {
                return NotFound();
               } 


               return Ok(areaModel);
         }

          [HttpDelete]
         [Route("Delete-areas/{id:guid}")]
         public async Task<IActionResult> Delete ([FromRoute] Guid id)
         {

            // var isAssignedToReservation = await _IReservaRepository.CountActiveReservationsByUserAsync(id);
            //  if (isAssignedToReservation)
            // {
            //   return BadRequest("No se puede eliminar, ya que está asignado a una reserva activa.");
            // }
            var area = await _areaRepository.DeleteAsync(id);

            if(area == null)
            {
                return NotFound();
            }
            
            return NoContent();
             
         }
    }
}