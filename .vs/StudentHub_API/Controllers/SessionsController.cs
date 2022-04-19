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
    [Route("api/session")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionsController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [SwaggerOperation(Tags = new[] { "sessions" })]
        [HttpPost("tutors/{tutorId}/users/{userId}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSessionResource resource, int tutorId,int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.SaveAsync(session, tutorId,userId);

            if (!result.Success)
                return BadRequest(result.Message);
            var sessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(sessionResource);
        }

        [SwaggerOperation(Tags = new[] { "sessions" })]
        [HttpPut("{sessionId}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int sessionId, [FromBody] SaveSessionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.UpdateAsync(sessionId, session);

            if (!result.Success)
                return BadRequest(result.Message);
            var sessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(sessionResource);
        }

        [SwaggerOperation(Tags = new[] { "sessions" })]
        [HttpGet("{sessionId}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int sessionId)
        {
            var result = await _sessionService.GetByIdAsync(sessionId);

            if (!result.Success)
                return BadRequest(result.Message);

            var sessionResource = _mapper.Map<Session, SessionResource>(result.Resource);

            return Ok(sessionResource);
        }
        [HttpGet("users/{userId}/sessions")]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllByUserIdAsync(int userId)
        {
            var users = await _sessionService.ListByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(users);

            return resources;
        }
        [SwaggerOperation(Tags = new[] { "sessions" })]
        [HttpDelete("{sessionId}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int sessionId)
        {
            var result = await _sessionService.DeleteAsync(sessionId);
            if (!result.Success)
                return BadRequest(result.Message);
            var sessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(sessionResource);
        }
    }
}
