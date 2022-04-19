using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Services.Comunications
{
    public class SessionResponse : BaseResponse<Session>
    {
        public SessionResponse(Session resource) : base(resource)
        {

        }

        public SessionResponse(string message) : base(message)
        {

        }
    }
}
