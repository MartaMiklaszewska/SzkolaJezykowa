using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test2Admin.Models;

namespace Test2Admin.Services
{
    public class SecurityDAO
    {
        internal bool FindAdmin(User name)
        {
            return (name.Name == "admin@admin.com" && name.Password == "Secret" && name.Role == "admin");
        }

        internal bool FindStudent(User role)
        {
            return (role.Role == "student");
           
        }

        internal bool FindTeacher(User role)
        {
            return (role.Role == "teacher");

        }

       
    }
}