using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPersonneRepository
{
    public class Personne
    {
        public int Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string? Work { get; set; }
        public string Country { get; set; }
    }
}
