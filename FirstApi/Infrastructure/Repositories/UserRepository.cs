using FirstApi.Domain.Models;
using FirstApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllPagedAsync(int skip, int take, string? nome, string? email)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                nome = nome.Trim().ToLower();
                query = query.Where(u => u.Nome.ToLower().Contains(nome));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                email = email.Trim().ToLower();
                query = query.Where(u => u.Email.ToLower().Contains(email));
            }

            return await query
                .OrderBy(u => u.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> CountAsync(string? nome, string? email)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(u => u.Nome.Contains(nome));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(u => u.Email.Contains(email));
            }

            return await query.CountAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}