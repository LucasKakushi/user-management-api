using FirstApi.Application.Dtos;
using FirstApi.Domain.Models;
using FirstApi.Infrastructure.Repositories;

namespace FirstApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedResultDto<UserResponseDto>> GetAllAsync(UserQueryParametersDto parameters)
        {
            int page = parameters.Page < 1 ? 1 : parameters.Page;
            int pageSize = parameters.PageSize < 1 ? 10 : parameters.PageSize > 50 ? 50 : parameters.PageSize;

            int skip = (page - 1) * pageSize;

            var totalCount = await _userRepository.CountAsync(parameters.Nome, parameters.Email);
            var users = await _userRepository.GetAllPagedAsync(skip, pageSize, parameters.Nome, parameters.Email);

            var items = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            });

            return new PagedResultDto<UserResponseDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email
            };
        }

        public async Task<User> CreateAsync(CreateUserDto dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLower();
            var normalizedRole = dto.Role.Trim().ToLower() == "admin" ? "Admin" : "User";

            if (await _userRepository.EmailExistsAsync(normalizedEmail))
                throw new InvalidOperationException("Email já existe");

            var user = new User
            {
                Nome = dto.Nome.Trim(),
                Email = normalizedEmail,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                Role = normalizedRole
            };

            return await _userRepository.CreateAsync(user);
        }

        public async Task<UserResponseDto?> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            var normalizedEmail = dto.Email.Trim().ToLower();
            var normalizedRole = dto.Role.Trim().ToLower() == "admin" ? "Admin" : "User";

            var existingUser = await _userRepository.GetByEmailAsync(normalizedEmail);

            if (existingUser != null && existingUser.Id != id)
                throw new InvalidOperationException("Email já existe");

            user.Nome = dto.Nome.Trim();
            user.Email = normalizedEmail;
            user.Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
            user.Role = normalizedRole;

            await _userRepository.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            await _userRepository.DeleteAsync(user);
            return true;
        }
    }
}