using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;

namespace CorporativeSN.Api.Profiles
{
    public class UsersListProfile : Profile
    {
        public UsersListProfile()
        {
            CreateMap<Users, UsersListDTO>()
                .ForMember(x=>x.Role, opt=>opt.MapFrom(c=>c.UserType.Name))
                .ForMember(x=>x.DepartmentName, opt=>opt.MapFrom(c=>c.Department.Name))
                .ForMember(x => x.ImageData, opt => opt.MapFrom(c => "" + Convert.ToBase64String(c.ImageData))); 
            CreateMap<UsersListDTO, Users>()
                .ForMember(x => x.ImageData, opt => opt.ConvertUsing(new Base64ByteConverter(), c => c.ImageData));
        }
    }
}
