using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Services
{
    public class FdService : IFdService
    {
        private readonly IFdRepository _repo;
        private readonly IMapper _mapper;

        public FdService(IFdRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> ApplyForFdAsync(ApplyFdDto dto)
        {
            var fd = _mapper.Map<Fdapplication>(dto);
            fd.Status = "Pending";
            fd.CreatedAt = DateTime.UtcNow;
            fd.IsActive = true;
            return await _repo.AddFdApplicationAsync(fd);
        }

        public async Task<FdStatusDto?> GetFdStatusAsync(int fdApplicationId)
        {
            var fd = await _repo.GetFdApplicationByIdAsync(fdApplicationId);
            return fd != null ? _mapper.Map<FdStatusDto>(fd) : null;
        }

        public async Task<IEnumerable<FdStatusDto>> GetAllFdsForUserAsync(int userId)
        {
            var fds = await _repo.GetFdsByUserIdAsync(userId);
            return fds.Select(_mapper.Map<FdStatusDto>);
        }

        public async Task<IEnumerable<FdPaymentScheduleDto>> GetFdPaymentScheduleAsync(int fdApplicationId)
        {
            var fdApp = await _repo.GetFdApplicationByIdAsync(fdApplicationId);
            if (fdApp == null) return Enumerable.Empty<FdPaymentScheduleDto>();
            return fdApp.Fdtransactions.Select(_mapper.Map<FdPaymentScheduleDto>);
        }
    }
} 