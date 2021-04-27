using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;

namespace CorporativeSN.Api.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<UsersMessages, MessageDTO>();
            CreateMap<MessageDTO, UsersMessages>();
        }
    }
}
