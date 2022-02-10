using Microsoft.AspNetCore.Mvc.Filters;

namespace BlazorApplication.Interfaces
{
    public interface IExceptionFilter
    {
        void OnException(ExceptionContext filterContext);
    }
}
