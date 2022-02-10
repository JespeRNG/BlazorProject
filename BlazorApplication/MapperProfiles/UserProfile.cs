using AutoMapper;
using BlazorApplication.DTO;
using BlazorApplication.Model;

namespace BlazorApplication.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            #region To DTO
            CreateMap<LoginUserInputModel, UserDTO>();
            CreateMap<RegistrationUserInputModel, UserDTO>();
            #endregion

            #region From DTO
            CreateMap<UserDTO, UserModel>();
            #endregion
        }
    }
}
