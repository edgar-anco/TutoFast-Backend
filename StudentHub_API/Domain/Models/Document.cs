using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relationships
        //Carrer
        public int CareerId { get; set; }
        public Career Career { get; set; }
        //Course
        public int CourseId { get; set; }
        public Course Course { get; set; }
        //User
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
