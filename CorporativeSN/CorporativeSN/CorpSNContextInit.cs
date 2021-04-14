using CorporativeSN.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Api
{
    public static class CorpSNContextInit
    {
        public static void Init(CorpSNContext context)
        {
            if (!context.UserTypes.Any())
            {
                context.UserTypes.Add(new UserType
                {
                    Type="Admin"
                });
                context.UserTypes.Add(new UserType
                {
                    Type = "User"
                });
                context.SaveChanges();
            }
        }
    }
}
