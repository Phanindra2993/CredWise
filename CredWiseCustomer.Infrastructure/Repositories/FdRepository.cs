using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;
using CredWiseCustomer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CredWiseCustomer.Infrastructure.Repositories
{
    public class FdRepository : IFdRepository
    {
        private readonly AppDbContext _context;
        public FdRepository(AppDbContext context) { _context = context; }

        public async Task<int> AddFdApplicationAsync(Fdapplication fd)
        {
            _context.Fdapplications.Add(fd);
            await _context.SaveChangesAsync();
            return fd.FdapplicationId;
        }

        public async Task<Fdapplication?> GetFdApplicationByIdAsync(int fdApplicationId)
        {
            return await _context.Fdapplications
                .Include(f => f.Fdtype)
                .FirstOrDefaultAsync(f => f.FdapplicationId == fdApplicationId);
        }

        public async Task<IEnumerable<Fdapplication>> GetFdsByUserIdAsync(int userId)
        {
            return await _context.Fdapplications
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
    }
} 