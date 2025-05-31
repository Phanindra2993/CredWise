using System.ComponentModel.DataAnnotations;

namespace CredWiseCustomer.Application.DTOs;

public class BaseLoanApplicationDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int LoanProductId { get; set; }

    [Required]
    [Range(1000, double.MaxValue, ErrorMessage = "Amount must be at least 1000")]
    public decimal RequestedAmount { get; set; }

    [Required]
    [Range(1, 360, ErrorMessage = "Tenure must be between 1 and 360 months")]
    public int RequestedTenure { get; set; }

    [Required]
    public string Gender { get; set; } = null!;

    [Required]
    public DateTime DOB { get; set; }

    [Required]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhaar must be exactly 12 digits")]
    [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhaar must contain only digits")]
    public string Aadhaar { get; set; } = null!;

    [Required]
    [StringLength(500)]
    public string Address { get; set; } = null!;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Income must be greater than 0")]
    public decimal Income { get; set; }

    [Required]
    [RegularExpression("^(Salaried|Self-Employed)$", ErrorMessage = "Employment type must be either 'Salaried' or 'Self-Employed'")]
    public string EmploymentType { get; set; } = null!;

    [Required]
    public string CreatedBy { get; set; } = null!;
}

public class GoldLoanApplicationDto : BaseLoanApplicationDto
{
    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Gold weight must be greater than 0")]
    public decimal GoldWeight { get; set; }

    [Required]
    [RegularExpression("^(22K|24K)$", ErrorMessage = "Gold purity must be either '22K' or '24K'")]
    public string GoldPurity { get; set; } = null!;
}

public class HomeLoanApplicationDto : BaseLoanApplicationDto
{
    [Required]
    [StringLength(500)]
    public string PropertyAddress { get; set; } = null!;

    [Required]
    [Range(0, 100, ErrorMessage = "Down payment percentage must be between 0 and 100")]
    public decimal DownPaymentPercentage { get; set; }
}

public class PersonalLoanApplicationDto : BaseLoanApplicationDto
{
    // No additional fields required for personal loan
}

public class LoanApplicationResponseDto
{
    public int LoanApplicationId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? DecisionDate { get; set; }
    public string? DecisionReason { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? LoanType { get; set; }
    public int UserId { get; set; }
    public string Gender { get; set; } = null!;
    public int LoanProductId { get; set; }
    public decimal RequestedAmount { get; set; }
    public int RequestedTenure { get; set; }
    public decimal InterestRate { get; set; }
} 