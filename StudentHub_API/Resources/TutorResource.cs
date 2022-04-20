using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Resources
{
    public class TutorResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double PricePerHour { get; set; }
        public string Url { get; set; }
    }
}
