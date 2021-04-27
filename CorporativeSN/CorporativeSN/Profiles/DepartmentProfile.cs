using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;

namespace CorporativeSN.Api.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Departments, DepartmentDTO>();
            CreateMap<DepartmentDTO, Departments>();
        }
    }
}
