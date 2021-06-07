using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test2Admin.Models;

namespace Test2Admin.Services
{
    public class SecurityService
    {
        SecurityDAO daoService = new SecurityDAO();
        public bool AuthenticateAdmin(User name)
        {
            return daoService.FindAdmin(name);
        }
        public bool AuthenticateStudent(User role)
        {
            return daoService.FindStudent(role);
        }

        public bool AuthenticateTeacher(User role)
        {
            return daoService.FindTeacher(role);
        }
    }
}