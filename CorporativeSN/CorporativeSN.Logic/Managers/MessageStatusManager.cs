using AutoMapper;
using CorporativeSN.Data;
using CorporativeSN.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Logic.Managers
{
    public class MessageStatusManager : IMessageStatusManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;

        public MessageStatusManager(ICorpSNContext corpSNContext, IMapper mapper)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
        }

    }
}
