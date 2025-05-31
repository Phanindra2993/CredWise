using CredWiseCustomer.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CredWiseCustomer.Api.Controllers
{
    public class OptionsController : ControllerBase
    {
        [HttpGet("genders")]
        [ProducesResponseType(typeof(List<OptionDto>), StatusCodes.Status200OK)]
        public IActionResult GetGenderOptions()
        {
            var options = new List<OptionDto>
     {
         new("Male", "Male"),
         new("Female", "Female"),
         new("Other", "Other")
     };
            return Ok(options);
        }

        [HttpGet("employment-types")]
        [ProducesResponseType(typeof(List<OptionDto>), StatusCodes.Status200OK)]
        public IActionResult GetEmploymentTypeOptions()
        {
            var options = new List<OptionDto>
     {
         new("Salaried", "Salaried"),
         new("Self-Employed", "Self-Employed"),
         new("Other", "Other")
     };
            return Ok(options);
        }

        [HttpGet("payment-types")]
        [ProducesResponseType(typeof(List<OptionDto>), StatusCodes.Status200OK)]
        public IActionResult GetPaymentTypeOptions()
        {
            var options = new List<OptionDto>
     {
         new("UPI", "UPI"),
         new("Net-Banking", "Net-Banking"),
         new("Card", "Card")
     };
            return Ok(options);
        }
    }
}
