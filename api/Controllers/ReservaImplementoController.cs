using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.ReservaImplemento;
using api.Controllers.Mappers;
using api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/reservaImplemento")]
    public class ReservaImplementoController : ControllerBase
    {
        private readonly IReservasImplementosRepository _reservaImplementoRepository;
        public ReservaImplementoController(IReservasImplementosRepository reservaImplementoRepository)
        {
            _reservaImplementoRepository = reservaImplementoRepository;
        }
          [HttpGet("all-reservaImplemento/{id:guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid id)
        {
            var reserva = await _reservaImplementoRepository.GetAllAsync(id);

            if(reserva == null)
            {
                return NotFound();
            } 
             var reservaDto =  reserva.Select(i => i.ToReservasImplementoDto());

            return Ok(reservaDto);
        } 

         [HttpGet("all-reservaImplementoUser/{id:guid}")]
        public async Task<IActionResult> GetAllByUser([FromRoute] Guid id)
        {
            var reserva = await _reservaImplementoRepository.GetAllByUserAsync(id);

            if(reserva == null)
            {
                return NotFound();
            } 
             var reservaDto =  reserva.Select(i => i.ToReservasImplementoUserDto());
            return Ok(reserva);
        } 

      [HttpPost ("Create-reservaImplemento")]
        public async Task<IActionResult> Create(CreateReservaImplementoRequestDto reservaDto)
        {

             if (!ModelState.IsValid)
                 return BadRequest(ModelState); 

                 var activeReservationsCount = await _reservaImplementoRepository.CountActiveReservationsByUserAsync(reservaDto.UserId);
                  if (activeReservationsCount >= 3)
                          return BadRequest("El usuario ya tiene el máximo de 3 reservas activas");

            
                var ReservareaModel = reservaDto.ToReservatFromCreate();
                await _reservaImplementoRepository.CreateAsync(ReservareaModel);
                return CreatedAtAction(nameof(GetById), new{id = ReservareaModel.Id}, ReservareaModel);
            
        } 

          [HttpGet("GetById-reservaImplemento/{Id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var reserva = await _reservaImplementoRepository.GetByIdAsync(Id);
            
            if(reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);

        }

         [HttpDelete]
        [Route("delete-reservaImplemento/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var  reserva = await _reservaImplementoRepository.GetByIdAsync(id);
             if(reserva == null)
            {
                return NotFound();
            }

            await _reservaImplementoRepository.DeleteAsync(id);
         
            return NoContent();
        }

    }
}