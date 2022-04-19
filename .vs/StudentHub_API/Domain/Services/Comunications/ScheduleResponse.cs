using StudentHub_API.Domain.Models;
using StudentHub_API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Services.Comunications
{
    public class ScheduleResponse : BaseResponse<Schedule>
    {
        public ScheduleResponse(Schedule resource) : base(resource)
        {

        }

        public ScheduleResponse(string message) : base(message)
        {

        }
    }
}
