using System.Net;
using System.Threading.Tasks;
using BlazorApplication.Model;
using System.Collections.Generic;

namespace BlazorApplication.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> GetCurrentUserInfoAsync();
        Task<List<UserModel>> GetListOfUsersAsync();
        Task<HttpStatusCode> DeleteUser(long id);
    }
}
