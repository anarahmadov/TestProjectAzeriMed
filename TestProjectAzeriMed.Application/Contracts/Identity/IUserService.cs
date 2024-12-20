﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestProjectAzeriMed.Application.Models.Identity;

namespace TestProjectAzeriMed.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int userId);
    }
}
