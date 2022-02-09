using System.Net;
using System.Threading.Tasks;
using BlazorApplication.Model;

namespace BlazorApplication.Interfaces
{
    public interface IAccountService
    {
        Task<HttpStatusCode> RegisterUserAsync(RegistrationUserInputModel model);
        Task<string> AuthorizeUserAsync(LoginUserInputModel model);
    }
}
