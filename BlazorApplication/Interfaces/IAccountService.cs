using System.Net;
using BlazorApplication.DTO;
using System.Threading.Tasks;

namespace BlazorApplication.Interfaces
{
    public interface IAccountService
    {
        Task<HttpStatusCode> RegisterUserAsync(UserDTO userDTO);
        Task<string> AuthorizeUserAsync(UserDTO userDTO);
    }
}
