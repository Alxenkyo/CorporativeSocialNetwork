using AutoMapper;
using CorporativeSN.Data;
using CorporativeSN.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Logic.Managers
{
    public class UserTypeManager : IUserTypeManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;

        public UserTypeManager(ICorpSNContext corpSNContext, IMapper mapper)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
        }
    }
}
