using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Resources
{
    public class SaveTutorResource
    {
        [Required]
        [MaxLength(120)]
        public string Description { get; set; }
        [Required]
        public double PricePerHour { get; set; }
        [Required]
        public string Url { get; set; }

    }
}
