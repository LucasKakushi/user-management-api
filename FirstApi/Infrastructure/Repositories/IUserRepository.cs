using FirstApi.Domain.Models;

namespace FirstApi.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllPagedAsync(int skip, int take, string? nome, string? email);
        Task<int> CountAsync(string? nome, string? email);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task<User> CreateAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
    }
}