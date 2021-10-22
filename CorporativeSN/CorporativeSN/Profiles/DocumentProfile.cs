using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CorporativeSN.Api.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Documents, DocumentDTO>();
            CreateMap<DocumentDTO, Documents>();
        }
    }
}
