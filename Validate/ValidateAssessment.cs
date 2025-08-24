using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Validate
{
    public class ValidateAssessment
    {
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; } = string.Empty;
        }

        public static ValidationResult ValidateAssessmentInput(
            string termName,
            object selectedType,
            DateTime startDate,
            DateTime endDate
            )

        {
            if (string.IsNullOrWhiteSpace(termName))
                return new ValidationResult { IsValid = false, ErrorMessage = "Course name is required." };

            if (selectedType == null)
                return new ValidationResult { IsValid = false, ErrorMessage = "Please select a course status." };

            if (startDate >= endDate)
                return new ValidationResult { IsValid = false, ErrorMessage = "End date must be after start date." };

            return new ValidationResult { IsValid = true };
        }
    }
}
