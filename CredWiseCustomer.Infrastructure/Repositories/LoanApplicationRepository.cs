using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;

using CredWiseCustomer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CredWiseCustomer.Infrastructure.Repositories;

public class LoanApplicationRepository : ILoanApplicationRepository
{
    private readonly AppDbContext _context;

    public LoanApplicationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoanApplication> GetByIdAsync(int id)
    {
        return await _context.LoanApplications
            .Include(x => x.LoanProduct)
            .Include(x => x.GoldLoanApplications)
            .Include(x => x.HomeLoanApplications)
            .FirstOrDefaultAsync(x => x.LoanApplicationId == id);
    }

    public async Task<IEnumerable<LoanApplication>> GetByUserIdAsync(int userId)
    {
        return await _context.LoanApplications
            .Include(x => x.LoanProduct)
            .Include(x => x.GoldLoanApplications)
            .Include(x => x.HomeLoanApplications)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<LoanApplication> AddAsync(LoanApplication loanApplication)
    {
        await _context.LoanApplications.AddAsync(loanApplication);
        await _context.SaveChangesAsync();
        return loanApplication;
    }

    public async Task<GoldLoanApplication> AddGoldLoanApplicationAsync(GoldLoanApplication goldLoanApplication)
    {
        await _context.GoldLoanApplications.AddAsync(goldLoanApplication);
        await _context.SaveChangesAsync();
        return goldLoanApplication;
    }

    public async Task<HomeLoanApplication> AddHomeLoanApplicationAsync(HomeLoanApplication homeLoanApplication)
    {
        await _context.HomeLoanApplications.AddAsync(homeLoanApplication);
        await _context.SaveChangesAsync();
        return homeLoanApplication;
    }

    public async Task<LoanProduct> GetLoanProductByIdAsync(int id)
    {
        return await _context.LoanProducts
            .FirstOrDefaultAsync(x => x.LoanProductId == id && x.IsActive);
    }

    public async Task<bool> UpdateAsync(LoanApplication loanApplication)
    {
        _context.LoanApplications.Update(loanApplication);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var loanApplication = await _context.LoanApplications.FindAsync(id);
        if (loanApplication == null)
            return false;

        loanApplication.IsActive = false;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<IEnumerable<LoanApplication>> GetAllAsync()
    {
        return await _context.LoanApplications
            .Include(x => x.LoanProduct)
            .Include(x => x.GoldLoanApplications)
            .Include(x => x.HomeLoanApplications)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
} 