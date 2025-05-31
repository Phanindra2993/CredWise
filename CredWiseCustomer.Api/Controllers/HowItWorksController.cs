using Microsoft.AspNetCore.Mvc;
using CredWiseCustomer.Application.DTOs;
using System.Collections.Generic;

namespace CredWiseCustomer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HowItWorksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<HowItWorksStepDto>> GetHowItWorksSteps()
        {
            var steps = new List<HowItWorksStepDto>
            {
                new HowItWorksStepDto { StepNumber = 1, Title = "Apply for the Loan", Description = "Go to the Apply Now page, enter your details for the loan application and apply for the loan." },
                new HowItWorksStepDto { StepNumber = 2, Title = "Submit Your Documents", Description = "As per the requirements of documents, submit your documents and get the response shortly." },
                new HowItWorksStepDto { StepNumber = 3, Title = "Wait for the Approval", Description = "Once the documents are submitted, your loan approval takes a few hours only." },
                new HowItWorksStepDto { StepNumber = 4, Title = "Get Disbursal", Description = "Get the disbursal directly in your linked account and use the fund as per the requirements." }
            };
            return Ok(steps);
        }
    }
} 