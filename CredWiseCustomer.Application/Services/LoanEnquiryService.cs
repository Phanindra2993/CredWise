using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;
using System.Threading.Tasks;

namespace CredWiseCustomer.Application.Services
{
    public class LoanEnquiryService : ILoanEnquiryService
    {
        private readonly ILoanEnquiryRepository _repo;
        private readonly IMapper _mapper;

        public LoanEnquiryService(ILoanEnquiryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LoanEnquiryResponseDto> AddEnquiryAsync(LoanEnquiryRequestDto dto)
        {
            var entity = _mapper.Map<LoanEnquiry>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            var result = await _repo.AddAsync(entity);
            return _mapper.Map<LoanEnquiryResponseDto>(result);
        }
    }
} 