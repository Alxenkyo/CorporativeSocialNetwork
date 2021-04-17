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
            if (!context.Departments.Any())
            {
                context.Departments.Add(new Department
                {
                    Name = "Admin"
                });
                context.Departments.Add(new Department
                {
                    Name = "JavaDev"
                });
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Name = "Admin",
                    Login = "admin",
                    Password = "admin",
                    DepartmentId = 1,
                    UserTypeId = 1
                });
                context.Users.Add(new User
                {
                    Name = "kekw",
                    Login = "kekw",
                    Password = "kekw",
                    DepartmentId = 2,
                    UserTypeId = 2
                });
                context.SaveChanges();
            }

        }
    }
}
