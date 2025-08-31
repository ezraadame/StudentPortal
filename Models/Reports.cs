using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentPortal.Services;

namespace StudentPortal.Models
{
    [Table("Reports")]
    public class Reports
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string? CourseName { get; set; }
        public string? AssessmentName { get; set; }
        public string? Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
