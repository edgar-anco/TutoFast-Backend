using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Resources
{
    public class SaveUserResource
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        public bool Accepted { get; set; }
    }
}
