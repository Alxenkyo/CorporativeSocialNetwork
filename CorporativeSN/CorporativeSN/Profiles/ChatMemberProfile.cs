using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;

namespace CorporativeSN.Api.Profiles
{
    public class ChatMemberProfile : Profile
    {
        public ChatMemberProfile()
        {
            CreateMap<ChatMember, ChatMemberDTO>()
                .ForMember(x=>x.UserName, opt=>opt.MapFrom(
                    c=>c.User.FirstName + " " + c.User.LastName))
                .ForMember(x=>x.ChatName, opt=>opt.MapFrom(c=>c.Chat.Name));
            CreateMap<ChatMemberDTO, ChatMember>();
        }
    }
}
