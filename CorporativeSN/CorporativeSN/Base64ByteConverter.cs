using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Api.Profiles
{
    public class Base64ByteConverter : IValueConverter<string, byte[]>
    {
        public byte[] Convert(string sourceMember, ResolutionContext context)
        {
            if (sourceMember != null) return System.Convert.FromBase64String(sourceMember);
            else return null;
        }
    }
}
