using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Models
{
    public class Tutor
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double PricePerHour { get; set; }
        public string Url { get; set; }
        
        //relationships
        //Course
        public int CourseId { get; set; }
        public Course Course { get; set; }
        //Schedules
        public List<Schedule> Schedules { get; set; }
        //User
        public int UserId { get; set; }
        public User User { get; set; }
        //Sessions
        public List<Session> Sessions { get; set; }

    }
}
