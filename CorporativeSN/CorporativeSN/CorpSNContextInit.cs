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
                context.UserTypes.Add(new UserTypes
                {
                    Name="Admin"
                });
                context.UserTypes.Add(new UserTypes
                {
                    Name = "User"
                });
                context.SaveChanges();
            }
            if (!context.Departments.Any())
            {
                context.Departments.Add(new Departments
                {
                    Name = "Admin"
                });
                context.Departments.Add(new Departments
                {
                    Name = "JavaDev"
                });
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                context.Users.Add(new Users
                {
                    Name = "Admin",
                    Login = "admin",
                    Password = "admin",
                    DepartmentId = 1,
                    UserTypeId = 1
                });
                context.Users.Add(new Users
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
