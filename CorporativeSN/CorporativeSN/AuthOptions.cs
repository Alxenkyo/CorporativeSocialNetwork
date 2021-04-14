using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporativeSN.Api
{
    public class AuthOptions
    {
        public const string ISSUER = "CorpSNServer"; // издатель токена
        public const string AUDIENCE = "CorpSNClient"; // потребитель токена
        const string KEY = "3450duaaaaabdfgdudf";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
