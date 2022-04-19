using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Services;
using StudentHub_API.Extensions;
using StudentHub_API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;

        public SchedulesController(IScheduleService scheduleService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        [SwaggerOperation(Tags = new[] { "schedules" })]
        [HttpPost("tutors/{tutorId}")]
        [ProducesResponseType(typeof(ScheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveScheduleResource resource, int tutorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var schedule = _mapper.Map<SaveScheduleResource, Schedule>(resource);
            var result = await _scheduleService.SaveAsync(schedule, tutorId);

            if (!result.Success)
                return BadRequest(result.Message);
            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(result.Resource);
            return Ok(scheduleResource);
        }

        [SwaggerOperation(Tags = new[] { "schedules" })]
        [HttpPut("{scheduleId}")]
        [ProducesResponseType(typeof(ScheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int scheduleId, [FromBody] SaveScheduleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var schedule = _mapper.Map<SaveScheduleResource, Schedule>(resource);
            var result = await _scheduleService.UpdateAsync(scheduleId, schedule);

            if (!result.Success)
                return BadRequest(result.Message);
            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(result.Resource);
            return Ok(scheduleResource);
        }

        [SwaggerOperation(Tags = new[] { "schedules" })]
        [HttpGet("{scheduleId}")]
        [ProducesResponseType(typeof(ScheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int scheduleId)
        {
            var result = await _scheduleService.GetByIdAsync(scheduleId);

            if (!result.Success)
                return BadRequest(result.Message);

            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(result.Resource);

            return Ok(scheduleResource);
        }
        [HttpGet("tutors/{tutorId}/schedules")]
        [ProducesResponseType(typeof(IEnumerable<ScheduleResource>), 200)]
        public async Task<IEnumerable<ScheduleResource>> GetAllByTutorIdAsync(int tutorId)
        {
            var tutors = await _scheduleService.ListByTutorIdAsync(tutorId);
            var resources = _mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleResource>>(tutors);

            return resources;
        }
        [SwaggerOperation(Tags = new[] { "schedules" })]
        [HttpDelete("{scheduleId}")]
        [ProducesResponseType(typeof(ScheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int scheduleId)
        {
            var result = await _scheduleService.DeleteAsync(scheduleId);
            if (!result.Success)
                return BadRequest(result.Message);
            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(result.Resource);
            return Ok(scheduleResource);
        }
    }
}

