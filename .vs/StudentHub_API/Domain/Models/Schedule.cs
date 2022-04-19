using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Date { get; set; }

        //relación con Tutor
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
    }
}
