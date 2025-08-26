using SQLite;
using StudentPortal.Services;

namespace StudentPortal.Models
{
    [Table("Assessments")]
    public class Assessments
    {
        

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Type { get; set; }
        public bool NotificationOn { get; set; }

        public static async Task AddAssessment(int id, int courseId, string? name, DateTime startDate, DateTime endDate, string? type, bool notificationOn)
        {
            await DBService.Init();
            var assessment = new Assessments()
            {
                Id = id,
                CourseId = courseId,
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Type = type,
                NotificationOn = notificationOn
            };
            await DBService.InsertAssessment(assessment);
            
        }

        public static async Task EditAssessment(int id, int courseId, string? name, DateTime startDate, DateTime endDate, string? type, bool notificationOn)
        {
            await DBService.Init();
            var assessment = new Assessments()
            {
                Id = id,
                CourseId = courseId,
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Type = type,
                NotificationOn = notificationOn
            };
            await DBService.EditAssessment(assessment);
        }
    }
}