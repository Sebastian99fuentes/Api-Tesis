using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.ReservaArea;
using api.Controllers.Mappers;
using api.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/reservaArea")]
    public class ReservaAreaController : ControllerBase
    {
        private readonly IReservaAreaRepository _reservaAreaRepository;

        public ReservaAreaController(IReservaAreaRepository reservaAreaRepository)
        {
            _reservaAreaRepository = reservaAreaRepository;
        }

         [HttpGet("all-reservaArea/{id:guid}")]
          [Authorize]
        public async Task<IActionResult> GetAll([FromRoute] Guid id)
        {
        
            var reserva = await _reservaAreaRepository.GetAllAsync(id);

            if(reserva == null)
            {
                return NotFound();
            } 
             var reservaDto =  reserva.Select(i => i.ToReservasAreasDto());

            return Ok(reserva);
        } 

         [HttpGet("all-reservaAreaUser/{id:guid}")]
          [Authorize]
        public async Task<IActionResult> GetAllByUser([FromRoute] Guid id)
        {
            var reserva = await _reservaAreaRepository.GetAllByUserAsync(id);

            if(reserva == null)
            {
                return NotFound();
            } 
             var reservaDto =  reserva.Select(i => i.ToReservasAreasUserDto());
            return Ok(reservaDto);
        } 

         [HttpPost ("Create-reservaArea")]
          [Authorize]
        public async Task<IActionResult> Create(CreateReservaAreaRequestDto reservaDto)
        {
           
             if (!ModelState.IsValid)
                 return BadRequest(ModelState); 


              if (reservaDto.UserId != Guid.Parse("11111111-1111-1111-1111-111111111111"))
                 {
                     var activeReservationsCount = await _reservaAreaRepository.CountActiveReservationsByUserAsync(reservaDto.UserId);
                       if (activeReservationsCount >= 3)
                          return BadRequest("El usuario ya tiene el máximo de 3 reservas activas");
                }
            
            
                var ReservareaModel = reservaDto.ToReservatFromCreate();
                await _reservaAreaRepository.CreateAsync(ReservareaModel);
                return CreatedAtAction(nameof(GetById), new{id = ReservareaModel.Id}, ReservareaModel);
            
        } 

         [HttpGet("GetById-reservaArea/{Id:guid}")]
          [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var reserva = await _reservaAreaRepository.GetByIdAsync(Id);
            
            if(reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);

        }

        [HttpDelete]
         [Authorize]
        [Route("delete-reservaArea/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var  reserva = await _reservaAreaRepository.GetByIdAsync(id);
             if(reserva == null)
            {
                return NotFound();
            }

            await _reservaAreaRepository.DeleteAsync(id);
         
            return NoContent();
        }


    }
}