﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;

namespace CorporativeSN.Api.Profiles
{
    public class UserTypeProfile : Profile
    {
        public UserTypeProfile()
        {
            CreateMap<UserTypes, UserTypeDTO>();
            CreateMap<UserTypeDTO, UserTypes>();
        }
    }
}
