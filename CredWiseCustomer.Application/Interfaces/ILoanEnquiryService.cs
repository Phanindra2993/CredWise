using CredWiseCustomer.Application.DTOs;
using System.Threading.Tasks;

namespace CredWiseCustomer.Application.Interfaces;
public interface ILoanEnquiryService
{
    Task<LoanEnquiryResponseDto> AddEnquiryAsync(LoanEnquiryRequestDto dto);
} 