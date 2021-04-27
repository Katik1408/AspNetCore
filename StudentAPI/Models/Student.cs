using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace StudentAPI.Models
{
    public partial class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("Place")]
        public string Area { get; set; }
        public int? Age { get; set; }
        public int? Contact { get; set; }
    }
}
