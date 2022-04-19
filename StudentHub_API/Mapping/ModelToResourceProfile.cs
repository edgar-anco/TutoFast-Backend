using AutoMapper;
using StudentHub_API.Domain.Models;
using StudentHub_API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Career, CareerResource>();
            CreateMap<Course, CourseResource>();
            CreateMap<Document, DocumentResource>();
            CreateMap<Schedule, ScheduleResource>();
            CreateMap<Session, SessionResource>();
            CreateMap<Tutor, TutorResource>();
            CreateMap<User, UserResource>();

        }
    }
}
