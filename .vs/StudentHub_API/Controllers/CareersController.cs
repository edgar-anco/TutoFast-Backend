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
using Swashbuckle.Swagger;

namespace StudentHub_API.Controllers
{
    [Route("api/career")]
    [ApiController]
    public class CareersController : ControllerBase
    {
        private readonly ICareerService _careerService;
        private readonly IMapper _mapper;

        public CareersController(ICareerService careerService, IMapper mapper)
        {
            _careerService = careerService;
            _mapper = mapper;
        }

        [SwaggerOperation(Tags = new[] { "careers" })]
        [HttpPost]
        [ProducesResponseType(typeof(CareerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveCareerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var career = _mapper.Map<SaveCareerResource, Career>(resource);
            var result = await _careerService.SaveAsync(career);

            if (!result.Success)
                return BadRequest(result.Message);
            var careerResource = _mapper.Map<Career, CareerResource>(result.Resource);
            return Ok(careerResource);
        }

        [SwaggerOperation(Tags = new[] { "careers" })]
        [HttpPut("{careerId}")]
        [ProducesResponseType(typeof(CareerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int careerId, [FromBody] SaveCareerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var career = _mapper.Map<SaveCareerResource, Career>(resource);
            var result = await _careerService.UpdateAsync(careerId, career);

            if (!result.Success)
                return BadRequest(result.Message);
            var careerResource = _mapper.Map<Career, CareerResource>(result.Resource);
            return Ok(careerResource);
        }

        [SwaggerOperation(Tags = new[] { "careers" })]
        [HttpGet("{careerId}")]
        [ProducesResponseType(typeof(CareerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int careerId)
        {
            var result = await _careerService.GetByIdAsync(careerId);

            if (!result.Success)
                return BadRequest(result.Message);

            var careerResource = _mapper.Map<Career, CareerResource>(result.Resource);

            return Ok(careerResource);
        }

        [SwaggerOperation(Tags = new[] { "careers" })]
        [HttpDelete("{careerId}")]
        [ProducesResponseType(typeof(CareerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int careerId)
        {
            var result = await _careerService.DeleteAsync(careerId);
            if (!result.Success)
                return BadRequest(result.Message);
            var careerResource = _mapper.Map<Career, CareerResource>(result.Resource);
            return Ok(careerResource);
        }
    }
}
