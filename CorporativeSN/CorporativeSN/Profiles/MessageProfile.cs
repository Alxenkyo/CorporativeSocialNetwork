using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            CreateMap<UsersMessages, MessageDTO>().ForMember(x => x.CreatorName, opt => opt.MapFrom(c => c.User.FirstName + " " + c.User.LastName));
            CreateMap<MessageDTO, UsersMessages>();
            CreateMap<AttachmentDTO, MessagesAttachments>()
                //.ForMember(x => x.BinaryData, opt => opt.MapFrom(c =>c.StringData));
            .ForMember(x => x.BinaryData, opt => opt.ConvertUsing(new Base64ByteConverter(), c=>c.BinaryData));
            CreateMap<MessagesAttachments, AttachmentDTO>()
                .ForMember(x => x.BinaryData, opt => opt.MapFrom(c =>"" + Convert.ToBase64String(c.BinaryData)));
        }
    }
}
