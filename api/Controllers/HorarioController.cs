using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Horario;
using api.Controllers.Mappers;
using api.Interfaces;
using api.Repository;
using api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/horario")] 
    [ApiController]
    public class HorarioController : ControllerBase
    {
         private readonly IHorarioRepository _IHorarioRepository;
         private readonly IAreaRepository _IAreaRepository;
         private readonly IImplementosRepository _IImplementoRepository;
        public HorarioController(  IHorarioRepository horarioRepository, IAreaRepository areaRepository, IImplementosRepository implementoRepository)
        {
            _IHorarioRepository = horarioRepository;
            _IAreaRepository = areaRepository;
            _IImplementoRepository = implementoRepository;
        } 

        [HttpGet("GetAll-horarios{AoI:guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid AoI)
        {
            var horarios = await _IHorarioRepository.GetAllAsync(AoI);
            
            var horarioDto = horarios.Where(x => x.Disponible == true).Select(c => c.ToHorarioDto());

            return Ok(horarioDto);

        }

        [HttpGet("GetById-horario{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var horarios = await _IHorarioRepository.GetByIdAsync(id);
            
            if(horarios == null)
            {
                return NotFound();
            }

            return Ok(horarios.ToHorarioDto());

        }

        [HttpPost("Create-horario{AoI:guid}")]
        public async Task<IActionResult> Create([FromRoute] Guid AoI, CreateHorarioRequestDto horarioDto)
        {
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(horarioDto.EsImplemento)
            {
                if(!await _IImplementoRepository.Exist(AoI))
                    return BadRequest("Implemento no existe");

            } 
            else
            {
                 if(!await _IAreaRepository.Exist(AoI))
                     return BadRequest("Area no existe"); 

            }        
            
            var horarioModel = horarioDto.ToHorarioFromCreate(AoI,horarioDto.EsImplemento);

            await _IHorarioRepository.CreateAsync(horarioModel);

            return CreatedAtAction(nameof(GetById), new { id = horarioModel.Id }, horarioModel.ToHorarioDto());
        }


        [HttpPut]
        [Route("Update-horario{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            
             // Verificar si el horario existe
             var horario = await _IHorarioRepository.GetByIdAsync(id);
              if (horario == null)
                   return NotFound("El horario no existe");

              var updatedHorario = await _IHorarioRepository.UpdateAsync(id);
  

                if (updatedHorario == null)
                     return StatusCode(500, "No se pudo actualizar el horario"); 

            return Ok(horario.ToHorarioDto());
        }


        [HttpDelete]
        [Route("Delete-horario{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        { 

            var horarioModel = await _IHorarioRepository.DeleteAsync(id);

            if(horarioModel == null)
            {
                return NotFound("Comment does not exist");
            }

            return Ok();

        }

    }
}