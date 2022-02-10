using System.Net;
using BlazorApplication.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorApplication.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetCurrentUserInfoAsync();
        Task<List<UserDTO>> GetListOfUsersAsync();
        Task<HttpStatusCode> DeleteUser(long id);
    }
}
