using AutoMapper;
using CorporativeSN.Data;
using CorporativeSN.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Logic.Managers
{
    public class ChatMemberManager : IChatMemberManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;

        public ChatMemberManager(ICorpSNContext corpSNContext, IMapper mapper)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
        }
        //Create chatmember
        //Delete chatmember
        //Get chatmembers
        //Update chatmembers
    }
}
