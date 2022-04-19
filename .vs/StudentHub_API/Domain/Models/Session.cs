using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //relación con User y tutor
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
