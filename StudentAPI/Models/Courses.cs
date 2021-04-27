using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Models
{
    public partial class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Instructor { get; set; }

        public string Duration { get; set; }
    }
}
