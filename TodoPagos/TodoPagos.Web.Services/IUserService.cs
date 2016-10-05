﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        void Dispose();
    }
}
