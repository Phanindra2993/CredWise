using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Interfaces
{
    public interface IFdRepository
    {
        Task<int> AddFdApplicationAsync(Fdapplication fd);
        Task<Fdapplication?> GetFdApplicationByIdAsync(int fdApplicationId);
        Task<IEnumerable<Fdapplication>> GetFdsByUserIdAsync(int userId);
    }
} 