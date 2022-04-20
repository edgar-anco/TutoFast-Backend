using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Resources
{
    public class SaveCareerResource
    {
     
        [Required]
        public string Name { get; set; }
    }
}
