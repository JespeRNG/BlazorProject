using BlazorApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApplication.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> GetCurrentUserInfoAsync();
    }
}
