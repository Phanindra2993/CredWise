using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Interfaces
{
    public interface ILoanEnquiryRepository
    {
        Task<LoanEnquiry> AddAsync(LoanEnquiry enquiry);
        //Task<LoanEnquiry> AddEnquiryAsync(LoanEnquiry enquiry);
        //Task<LoanEnquiry> GetEnquiryByIdAsync(int id);
        //Task<IEnumerable<LoanEnquiry>> GetEnquiriesByUserIdAsync(int userId);
    }
}
