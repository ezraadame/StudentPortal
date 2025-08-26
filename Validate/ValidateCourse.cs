using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentPortal.Validate
{
    public static class ValidateCourse
    {
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; } = string.Empty;
        }

        public static ValidationResult ValidateCourseInput(
            string courseName,
            string instructorName,
            object selectedStatus,
            DateTime startDate,
            DateTime endDate,
            string instructorPhone = "",
            string instructorEmail = "")
        {
            if (string.IsNullOrWhiteSpace(courseName))
                return new ValidationResult { IsValid = false, ErrorMessage = "Course name is required." };

            if (string.IsNullOrWhiteSpace(instructorName))
                return new ValidationResult { IsValid = false, ErrorMessage = "Instructor name is required." };

            if (selectedStatus == null)
                return new ValidationResult { IsValid = false, ErrorMessage = "Please select a course status." };

            if (startDate >= endDate)
                return new ValidationResult { IsValid = false, ErrorMessage = "End date must be after start date." };
            
            if (!string.IsNullOrWhiteSpace(instructorPhone))
            {
                if (!IsValidPhoneNumber(instructorPhone.Trim()))
                    return new ValidationResult { IsValid = false, ErrorMessage = "Please enter a valid phone number." };
            }
            if (!string.IsNullOrWhiteSpace(instructorEmail))
            {
                if (!IsValidEmail(instructorEmail.Trim()))
                    return new ValidationResult { IsValid = false, ErrorMessage = "Please enter a valid email address." };
            }
            return new ValidationResult { IsValid = true };
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return true;

            string digitsOnly = Regex.Replace(phone, @"[^\d]", "");

            return Regex.IsMatch(digitsOnly, @"^\d{10}$");
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true;

            try
            {
                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(email))
                    return false;

                var mailAddress = new System.Net.Mail.MailAddress(email);
                if (mailAddress.Address != email)
                    return false;
                var allowedDomains = new[] { ".com", ".edu", ".org", ".net", ".gov" };
                string emailLower = email.ToLower();
                return allowedDomains.Any(domain => emailLower.EndsWith(domain));
            }
            catch
            {
                return false;
            }
        }
    }
}
