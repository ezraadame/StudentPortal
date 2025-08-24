using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Validate
{
    public class ValidateTerm
    {
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; } = string.Empty;
        }
        public static ValidationResult ValidateTermInput(
            string termName,
            DateTime startDate,
            DateTime endDate)
            
        {
            if (string.IsNullOrWhiteSpace(termName))
                return new ValidationResult { IsValid = false, ErrorMessage = "Course name is required." };

            if (startDate >= endDate)
                return new ValidationResult { IsValid = false, ErrorMessage = "End date must be after start date." };

            return new ValidationResult { IsValid = true };
        }
    }
}
