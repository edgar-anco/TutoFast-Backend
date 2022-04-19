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
    [Route("api/document")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;

        public DocumentsController(IDocumentService documentService, IMapper mapper)
        {
            _documentService = documentService;
            _mapper = mapper;
        }

        [SwaggerOperation(Tags = new[] { "documents" })]
        [HttpPost("users/{userId}/career/{careerId}/courses/{courseId}/documents")]
        [ProducesResponseType(typeof(DocumentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, int careerId, int courseId, [FromBody] SaveDocumentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var document = _mapper.Map<SaveDocumentResource, Document>(resource);
            var result = await _documentService.SaveAsync(userId,careerId,courseId,document);

            if (!result.Success)
                return BadRequest(result.Message);
            var documentResource = _mapper.Map<Document, DocumentResource>(result.Resource);
            return Ok(documentResource);
        }

        [SwaggerOperation(
            Summary = "List Document",
            Description = "This endpoint returns all documents",
            OperationId = "ListDocument"
        )]
        [SwaggerResponse(200, "List of Documents", typeof(IEnumerable<DocumentResource>))]
        [HttpGet("documents")]
        [ProducesResponseType(typeof(IEnumerable<DocumentResource>), 200)]
        public async Task<IEnumerable<DocumentResource>> GetAllAsync()
        {
            var documents = await _documentService.ListAsync();
            var resorces = _mapper.Map<IEnumerable<Document>, IEnumerable<DocumentResource>>(documents);

            return resorces;
        }

        [HttpGet("courses/{courseId}/documents")]
        [ProducesResponseType(typeof(IEnumerable<DocumentResource>), 200)]
        public async Task<IEnumerable<DocumentResource>> GetAllByCourseIdAsync(int courseId)
        {
            var documents = await _documentService.ListByCourseIdAsync(courseId);
            var resources = _mapper.Map<IEnumerable<Document>, IEnumerable<DocumentResource>>(documents);

            return resources;
        }

        [HttpGet("careers/{careerId}/documents")]
        [ProducesResponseType(typeof(IEnumerable<DocumentResource>), 200)]
        public async Task<IEnumerable<DocumentResource>> GetAllByCareerIdAsync(int careerId)
        {
            var careers = await _documentService.ListByCareerIdAsync(careerId);
            var resources = _mapper.Map<IEnumerable<Document>, IEnumerable<DocumentResource>>(careers);

            return resources;
        }

        [SwaggerOperation(Tags = new[] { "documents" })]
        [HttpPut("{documentId}")]
        [ProducesResponseType(typeof(DocumentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int documentId, [FromBody] SaveDocumentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var document = _mapper.Map<SaveDocumentResource, Document>(resource);
            var result = await _documentService.UpdateAsync(documentId, document);

            if (!result.Success)
                return BadRequest(result.Message);
            var documentResource = _mapper.Map<Document, DocumentResource>(result.Resource);
            return Ok(documentResource);
        }

        [SwaggerOperation(Tags = new[] { "documents" })]
        [HttpGet("{documentId}")]
        [ProducesResponseType(typeof(DocumentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int documentId)
        {
            var result = await _documentService.GetByIdAsync(documentId);

            if (!result.Success)
                return BadRequest(result.Message);

            var documentResource = _mapper.Map<Document, DocumentResource>(result.Resource);

            return Ok(documentResource);
        }

        [SwaggerOperation(Tags = new[] { "documents" })]
        [HttpDelete("{documentId}")]
        [ProducesResponseType(typeof(DocumentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int documentId)
        {
            var result = await _documentService.DeleteAsync(documentId);
            if (!result.Success)
                return BadRequest(result.Message);
            var documentResource = _mapper.Map<Document, DocumentResource>(result.Resource);
            return Ok(documentResource);
        }
    }
}
