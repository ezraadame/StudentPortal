using SQLite;
using StudentPortal.Models;
using StudentPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.EvaluationData
{
    public class AnikaPatelData
    {
        public static async Task CreateEvaluationData(SQLiteAsyncConnection database)
        {
            var terms = await database.Table<Term>().ToListAsync();
            if (terms.Any(t => t.Name == "Fall 2025")) return;

            var term = new Term { Name = "Fall 2025", StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 12, 21) };
            await database.InsertAsync(term);

            var insertedTerm = await database.Table<Term>().OrderByDescending(t => t.Id).FirstAsync();

            var course = new Courses
            {
                TermId = insertedTerm.Id,
                Name = "Mobile Application Development",
                StartDate = new DateTime(2025, 9, 15),
                EndDate = new DateTime(2025, 12, 21),
                Status = "In Progress",
                InstructorName = "Anika Patel",
                InstructorPhone = "5551234567",
                InstructorEmail = "anika.patel@strimeuniversity.edu"
            };
            await database.InsertAsync(course);

            var insertedCourse = await database.Table<Courses>().OrderByDescending(c => c.Id).FirstAsync();

            await database.InsertAsync(new Assessments
            {
                CourseId = insertedCourse.Id,
                Name = "Quiz",
                Type = "objective",
                StartDate = new DateTime(2025, 9, 15),
                EndDate = new DateTime(2025, 12, 21),

            });

            await database.InsertAsync(new Assessments
            {
                CourseId = insertedCourse.Id,
                Name = "Final Project",
                Type = "performance",
                StartDate = new DateTime(2025, 9, 15),
                EndDate = new DateTime(2025, 12, 21),

            });
        }
    }
}
