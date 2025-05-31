using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _repo;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> ApplyForLoanAsync(ApplyLoanDto dto)
        {
            var loan = _mapper.Map<LoanApplication>(dto);
            loan.Status = "Initial Review";
            loan.CreatedAt = DateTime.UtcNow;
            loan.IsActive = true;
            await _repo.AddLoanApplicationAsync(loan);

            var loanProduct = await _repo.GetLoanProductByIdAsync(dto.LoanProductId);

            if (loanProduct.LoanType == "Gold")
            {
                var goldDetails = new GoldLoanDetail
                {
                    LoanProductId = loanProduct.LoanProductId,
                    InterestRate = Convert.ToDecimal(dto.AdditionalDetails["InterestRate"]),
                    TenureMonths = Convert.ToInt32(dto.AdditionalDetails["TenureMonths"]),
                    ProcessingFee = Convert.ToDecimal(dto.AdditionalDetails["ProcessingFee"]),
                    GoldPurityRequired = dto.AdditionalDetails["GoldPurityRequired"]?.ToString(),
                    RepaymentType = dto.AdditionalDetails["RepaymentType"]?.ToString(),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = dto.CreatedBy
                };
                await _repo.AddGoldLoanDetailAsync(goldDetails);
            }
            else if (loanProduct.LoanType == "Home")
            {
                var homeDetails = new HomeLoanDetail
                {
                    LoanProductId = loanProduct.LoanProductId,
                    InterestRate = Convert.ToDecimal(dto.AdditionalDetails["InterestRate"]),
                    TenureMonths = Convert.ToInt32(dto.AdditionalDetails["TenureMonths"]),
                    ProcessingFee = Convert.ToDecimal(dto.AdditionalDetails["ProcessingFee"]),
                    DownPaymentPercentage = Convert.ToDecimal(dto.AdditionalDetails["DownPaymentPercentage"]),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = dto.CreatedBy
                };
                await _repo.AddHomeLoanDetailAsync(homeDetails);
            }
            else if (loanProduct.LoanType == "Personal")
            {
                var personalDetails = new PersonalLoanDetail
                {
                    LoanProductId = loanProduct.LoanProductId,
                    InterestRate = Convert.ToDecimal(dto.AdditionalDetails["InterestRate"]),
                    TenureMonths = Convert.ToInt32(dto.AdditionalDetails["TenureMonths"]),
                    ProcessingFee = Convert.ToDecimal(dto.AdditionalDetails["ProcessingFee"]),
                    MinSalaryRequired = Convert.ToDecimal(dto.AdditionalDetails["MinSalaryRequired"]),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = dto.CreatedBy
                };
                await _repo.AddPersonalLoanDetailAsync(personalDetails);
            }

            return loan.LoanApplicationId;
        }

        public async Task<LoanStatusDto?> GetLoanStatusAsync(int loanApplicationId)
        {
            var loan = await _repo.GetLoanApplicationByIdAsync(loanApplicationId);
            return loan != null ? _mapper.Map<LoanStatusDto>(loan) : null;
        }

        public async Task<IEnumerable<LoanStatusDto>> GetAllLoansForUserAsync(int userId)
        {
            var loans = await _repo.GetLoansByUserIdAsync(userId);
            return loans.Select(_mapper.Map<LoanStatusDto>);
        }

        public async Task<IEnumerable<LoanStatusDto>> GetAllLoanApplicationsAsync()
        {
            var loans = await _repo.GetAllLoanApplicationsAsync();
            return loans.Select(_mapper.Map<LoanStatusDto>);
        }

        public async Task<IEnumerable<LoanProductDocumentDto>> GetRequiredDocumentsAsync(int loanProductId)
        {
            var docs = await _repo.GetRequiredDocumentsAsync(loanProductId);
            return docs.Select(doc => new LoanProductDocumentDto
            {
                LoanProductDocumentId = doc.LoanProductDocumentId,
                LoanProductId = doc.LoanProductId,
                DocumentName = doc.DocumentName,
                IsActive = doc.IsActive
            });
        }

        public async Task<bool> UploadLoanProductDocumentAsync(int loanProductId, int? loanApplicationId, string documentName, byte[] documentContent, string createdBy)
        {
            var doc = new LoanProductDocument
            {
                LoanProductId = loanProductId,
                LoanApplicationId = loanApplicationId,
                DocumentName = documentName,
                DocumentContent = documentContent,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };
            await _repo.AddLoanProductDocumentAsync(doc);
            return true;
        }
    }
} 