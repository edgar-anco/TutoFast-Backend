using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Accepted { get; set; }
        //relationships
        //Tutor
        public Tutor Tutor { get; set; }
        //Document
        public List<Document> Documents { get; set; }
        //Session
        public List<Session> Sessions { get; set; }
    }
}
