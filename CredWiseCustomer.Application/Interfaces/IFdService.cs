using CredWiseCustomer.Application.DTOs;

namespace CredWiseCustomer.Application.Interfaces
{
    public interface IFdService
    {
        Task<int> ApplyForFdAsync(ApplyFdDto dto);
        Task<FdStatusDto?> GetFdStatusAsync(int fdApplicationId);
        Task<IEnumerable<FdStatusDto>> GetAllFdsForUserAsync(int userId);
        Task<IEnumerable<FdPaymentScheduleDto>> GetFdPaymentScheduleAsync(int fdApplicationId);
    }
} 