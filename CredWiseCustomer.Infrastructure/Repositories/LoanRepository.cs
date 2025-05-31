using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;
using CredWiseCustomer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CredWiseCustomer.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;
        public LoanRepository(AppDbContext context) { _context = context; }

        public async Task<int> AddLoanApplicationAsync(LoanApplication loan)
        {
            _context.LoanApplications.Add(loan);
            await _context.SaveChangesAsync();
            return loan.LoanApplicationId;
        }

        public async Task<LoanApplication?> GetLoanApplicationByIdAsync(int loanApplicationId)
        {
            return await _context.LoanApplications.FindAsync(loanApplicationId);
        }

        public async Task<IEnumerable<LoanApplication>> GetLoansByUserIdAsync(int userId)
        {
            return await _context.LoanApplications
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoanApplication>> GetAllLoanApplicationsAsync()
        {
            return await _context.LoanApplications
                .Include(l => l.User)
                .Include(l => l.LoanProduct)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<LoanProduct> GetLoanProductByIdAsync(int loanProductId)
        {
            return await _context.LoanProducts.FindAsync(loanProductId);
        }

        public async Task AddGoldLoanDetailAsync(GoldLoanDetail detail)
        {
            _context.GoldLoanDetails.Add(detail);
            await _context.SaveChangesAsync();
        }

        public async Task AddHomeLoanDetailAsync(HomeLoanDetail detail)
        {
            _context.HomeLoanDetails.Add(detail);
            await _context.SaveChangesAsync();
        }

        public async Task AddPersonalLoanDetailAsync(PersonalLoanDetail detail)
        {
            _context.PersonalLoanDetails.Add(detail);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanProductDocument>> GetRequiredDocumentsAsync(int loanProductId)
        {
            return await _context.LoanProductDocuments
                .Where(d => d.LoanProductId == loanProductId && d.IsActive)
                .ToListAsync();
        }

        public async Task AddLoanBankStatementAsync(LoanBankStatement statement)
        {
            _context.LoanBankStatements.Add(statement);
            await _context.SaveChangesAsync();
        }

        public async Task AddLoanProductDocumentAsync(LoanProductDocument doc)
        {
            _context.LoanProductDocuments.Add(doc);
            await _context.SaveChangesAsync();
        }
    }
} 