using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Models
{
    public class Career
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Relationships
        //Document
        public List<Document> Documents { get; set; }
    }
}
