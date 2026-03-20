using FirstApi.Application.Dtos;
using FirstApi.Domain.Models;

namespace FirstApi.Application.Services
{
    public interface IUserService
    {
        Task<PagedResultDto<UserResponseDto>> GetAllAsync(UserQueryParametersDto parameters);
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<User> CreateAsync(CreateUserDto dto);
        Task<UserResponseDto?> UpdateAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteAsync(int id);
    }
}