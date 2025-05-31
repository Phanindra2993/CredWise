using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;


namespace CredWiseCustomer.Application.Services;

public class LoanApplicationService : ILoanApplicationService
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly IMapper _mapper;

    public LoanApplicationService(ILoanApplicationRepository loanApplicationRepository, IMapper mapper)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _mapper = mapper;
    }

    public async Task<LoanApplicationResponseDto> ApplyForGoldLoanAsync(GoldLoanApplicationDto application)
    {
        // Validate loan product type
        var loanProduct = await _loanApplicationRepository.GetLoanProductByIdAsync(application.LoanProductId);
        if (loanProduct == null)
            throw new KeyNotFoundException($"Loan product with ID {application.LoanProductId} not found");

        if (loanProduct.LoanType != "GOLD")
            throw new InvalidOperationException($"Invalid loan type. Expected GOLD but got {loanProduct.LoanType}");

        // Create base loan application
        var loanApplication = await CreateBaseLoanApplication(application);

        // Create gold loan specific details
        var goldLoanApplication = new GoldLoanApplication
        {
            LoanApplication = loanApplication,
            GoldWeight = application.GoldWeight,
            GoldPurity = application.GoldPurity
        };

        await _loanApplicationRepository.AddGoldLoanApplicationAsync(goldLoanApplication);
        return _mapper.Map<LoanApplicationResponseDto>(loanApplication);
    }

    public async Task<LoanApplicationResponseDto> ApplyForHomeLoanAsync(HomeLoanApplicationDto application)
    {
        // Validate loan product type
        var loanProduct = await _loanApplicationRepository.GetLoanProductByIdAsync(application.LoanProductId);
        if (loanProduct == null)
            throw new KeyNotFoundException($"Loan product with ID {application.LoanProductId} not found");

        if (loanProduct.LoanType != "HOME")
            throw new InvalidOperationException($"Invalid loan type. Expected HOME but got {loanProduct.LoanType}");

        // Create base loan application
        var loanApplication = await CreateBaseLoanApplication(application);

        // Create home loan specific details
        var homeLoanApplication = new HomeLoanApplication
        {
            LoanApplication = loanApplication,
            PropertyAddress = application.PropertyAddress,
            DownPaymentPercentage = application.DownPaymentPercentage
        };

        await _loanApplicationRepository.AddHomeLoanApplicationAsync(homeLoanApplication);
        return _mapper.Map<LoanApplicationResponseDto>(loanApplication);
    }

    public async Task<LoanApplicationResponseDto> ApplyForPersonalLoanAsync(PersonalLoanApplicationDto application)
    {
        // Validate loan product type
        var loanProduct = await _loanApplicationRepository.GetLoanProductByIdAsync(application.LoanProductId);
        if (loanProduct == null)
            throw new KeyNotFoundException($"Loan product with ID {application.LoanProductId} not found");

        if (loanProduct.LoanType != "PERSONAL")
            throw new InvalidOperationException($"Invalid loan type. Expected PERSONAL but got {loanProduct.LoanType}");

        // Create base loan application
        var loanApplication = await CreateBaseLoanApplication(application);
        return _mapper.Map<LoanApplicationResponseDto>(loanApplication);
    }

    public async Task<LoanApplicationResponseDto> GetLoanApplicationStatusAsync(int loanApplicationId)
    {
        var application = await _loanApplicationRepository.GetByIdAsync(loanApplicationId);
        if (application == null)
            throw new KeyNotFoundException($"Loan application with ID {loanApplicationId} not found");

        return _mapper.Map<LoanApplicationResponseDto>(application);
    }

    public async Task<IEnumerable<LoanApplicationResponseDto>> GetUserLoanApplicationsAsync(int userId)
    {
        var applications = await _loanApplicationRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<LoanApplicationResponseDto>>(applications);
    }

    public async Task<IEnumerable<LoanApplicationResponseDto>> GetAllLoanApplicationsAsync()
    {
        var applications = await _loanApplicationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<LoanApplicationResponseDto>>(applications);
    }

    private async Task<LoanApplication> CreateBaseLoanApplication(BaseLoanApplicationDto application)
    {
        var loanApplication = new LoanApplication
        {
            UserId = application.UserId,
            LoanProductId = application.LoanProductId,
            RequestedAmount = application.RequestedAmount,
            RequestedTenure = application.RequestedTenure,
            Gender = application.Gender,
            Dob = DateOnly.FromDateTime(application.DOB),
            Aadhaar = application.Aadhaar,
            Address = application.Address,
            Income = application.Income,
            EmploymentType = application.EmploymentType,
            Status = "Initial Review",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = application.CreatedBy
        };

        return await _loanApplicationRepository.AddAsync(loanApplication);
    }
} 