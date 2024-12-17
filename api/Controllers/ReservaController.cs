using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Implemento;
using api.Controllers.Dtos.Reserva;
using api.Controllers.Mappers;
using api.Interfaces;
using api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllerss
{
    [Route("api/reserva")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservasRepository _reservaRepository;

        private readonly IHorarioRepository _horarioRepository; 

        private readonly IImplementosRepository _implementoRepository;
        public ReservaController(IReservasRepository reservaRepository, IHorarioRepository horarioRepository, IImplementosRepository implementosRepository)
        {
            _reservaRepository =  reservaRepository;
            _horarioRepository = horarioRepository;
             _implementoRepository = implementosRepository;
        } 

        [HttpGet("all-reserva/{id:guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid id)
        {
            var reserva = await _reservaRepository.GetAllAsync(id);

            if(reserva == null)
            {
                return NotFound();
            } 

            
                var reservasDto = reserva.Select(r => new
                {
                    r.Id,
                    Horario = r.Horario.ToHorarioDto()
                }).ToList();

            return Ok(reservasDto);
        } 

        [HttpGet("GetById-reserva/{Id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(Id);
            
            if(reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);

        }

        [HttpPost ("Create-reserva")]
        public async Task<IActionResult> Create(CreateReservaRequestDto reservaDto)
        {

             if (!ModelState.IsValid)
                 return BadRequest(ModelState); 

                 var activeReservationsCount = await _reservaRepository.CountActiveReservationsByUserAsync(reservaDto.UserId);
                  if (activeReservationsCount >= 3)
                          return BadRequest("El usuario ya tiene el máximo de 3 reservas activas");

             var horario = await _horarioRepository.GetByIdAsync(reservaDto.HorarioId); 

            if(horario == null)
                return BadRequest("El horario no existe");

            var reservaModel = reservaDto.ToReservatFromCreate();

            await  _reservaRepository.CreateAsync(reservaModel); 
     
            await _horarioRepository.UpdateAsync(horario.Id); 

            if(horario.ImplementoId.HasValue)
            {
                 await _implementoRepository.UpdateImpleAsync(horario.ImplementoId, false);
            }

            return CreatedAtAction(nameof(GetById), new{id = reservaModel.Id}, reservaModel);
        } 

        [HttpDelete]
        [Route("delete-reserva/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var  reserva = await _reservaRepository.GetByIdAsync(id);
             if(reserva == null)
            {
                return NotFound();
            }

            var horario = await _horarioRepository.GetByIdAsync(reserva.HorarioId); 
             if(horario == null)
                return BadRequest("El horario no existe");

             await _horarioRepository.UpdateAsync(horario.Id);

            var reservaModel = await _reservaRepository.DeleteAsync(id);

            if(reservaModel == null)
            {
                return NotFound("Horario no existe");
            }
              if(horario.ImplementoId.HasValue)
            {
                 await _implementoRepository.UpdateImpleAsync(horario.ImplementoId, true);
            }
         
            return Ok();
        }


    }
}