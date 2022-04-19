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
    [Route("api/course")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CoursesController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [SwaggerOperation(Tags = new[] { "courses" })]
        [HttpPost]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveCourseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var course = _mapper.Map<SaveCourseResource, Course>(resource);
            var result = await _courseService.SaveAsync(course);

            if (!result.Success)
                return BadRequest(result.Message);
            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);
            return Ok(courseResource);
        }

        [SwaggerOperation(Tags = new[] { "courses" })]
        [HttpPut("{courseId}")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int courseId, [FromBody] SaveCourseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var course = _mapper.Map<SaveCourseResource, Course>(resource);
            var result = await _courseService.UpdateAsync(courseId, course);

            if (!result.Success)
                return BadRequest(result.Message);
            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);
            return Ok(courseResource);
        }

        [SwaggerOperation(Tags = new[] { "courses" })]
        [HttpGet("{courseId}")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int courseId)
        {
            var result = await _courseService.GetByIdAsync(courseId);

            if (!result.Success)
                return BadRequest(result.Message);

            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);

            return Ok(courseResource);
        }

        [SwaggerOperation(Tags = new[] { "courses" })]
        [HttpDelete("{courseId}")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int courseId)
        {
            var result = await _courseService.DeleteAsync(courseId);
            if (!result.Success)
                return BadRequest(result.Message);
            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);
            return Ok(courseResource);
        }
    }
}
