using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;

namespace CorporativeSN.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Users, UserDTO>();
            CreateMap<UserDTO, Users>();
        }
    }
}
