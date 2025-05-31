using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;
using CredWiseCustomer.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredWiseCustomer.Infrastructure.Repositories
{
    public class LoanEnquiryRepository : ILoanEnquiryRepository
    {
        private readonly AppDbContext _context;
        public LoanEnquiryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanEnquiry> AddAsync(LoanEnquiry enquiry)
        {
            _context.LoanEnquiries.Add(enquiry);
            await _context.SaveChangesAsync();
            return enquiry;
        }
    }
}
