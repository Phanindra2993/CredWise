using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredWiseCustomer.Application.DTOs
{
    public class OptionDto
    {
        public string Value { get; set; }
        public string Label { get; set; }

        public OptionDto(string value, string label)
        {
            Value = value;
            Label = label;
        }
    }
}
