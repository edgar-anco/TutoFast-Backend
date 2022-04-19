using AutoMapper;
using StudentHub_API.Domain.Models;
using StudentHub_API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCareerResource, Career>();
            CreateMap<SaveCourseResource, Course>();
            CreateMap<SaveDocumentResource, Document>();
            CreateMap<SaveScheduleResource, Schedule>();
            CreateMap<SaveSessionResource, Session>();
            CreateMap<SaveTutorResource, Tutor>();
            CreateMap<SaveUserResource, User>();
        }
    }
}
