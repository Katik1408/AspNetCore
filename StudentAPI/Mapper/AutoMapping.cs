using AutoMapper;
using StudentAPI.DataTransferObjects;
using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<StudentDto, Student>();
        }
    }
}
