using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Services.Comunications
{
    public class DocumentResponse : BaseResponse<Document>
    {
        public DocumentResponse(Document resource) : base(resource)
        {

        }

        public DocumentResponse(string message) : base(message)
        {

        }
    }
}
