using SQLite;
using StudentPortal.Services;

namespace StudentPortal.Models
{
    [Table("Courses")]
    public class Courses
    {
        

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TermId { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public string? InstructorName { get; set; }
        public string? InstructorPhone { get; set; }
        public string? InstructorEmail { get; set; }
        public string? Notes { get; set; }
        public bool NotificationOn { get; set; }

        public static async Task AddCourse(int id, int termId, string? name, DateTime startDate, DateTime endDate, string? status, string? instructorName, string? instructorPhone, string? instructorEmail, string? notes, bool notificationOn)
        {
            await DBService.Init();
            var course = new Courses()
            {
                Id = id,
                TermId = termId,
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                InstructorName = instructorName,
                InstructorPhone = instructorPhone,
                InstructorEmail = instructorEmail,
                Notes = notes,
                NotificationOn = notificationOn
            };
            await DBService.InsertCourse(course);

        }

        public static async Task EditCourse(int id, int termId, string? name, DateTime startDate, DateTime endDate, string? status, string? instructorName, string? instructorPhone, string? instructorEmail, string? notes, bool notificationOn)
        {
            await DBService.Init();
            var course = new Courses()
            {
                Id = id,
                TermId = termId,
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                InstructorName = instructorName,
                InstructorPhone = instructorPhone,
                InstructorEmail = instructorEmail,
                Notes = notes,
                NotificationOn = notificationOn
            };
            await DBService.EditCourse(course);

        }
    }



}

