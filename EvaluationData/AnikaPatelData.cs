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
            await CreateTestUser(database);
            var testUser = await database.Table<Users>()
                .Where(u => u.Username == "testuser")
                .FirstAsync();
            var terms = await database.Table<Term>()
                .Where(t => t.Name == "Fall 2025" && t.UserId == testUser.Id)
                .ToListAsync();

            if (terms.Any()) return;

            var term = new Term
            {
                Name = "Fall 2025",
                StartDate = new DateTime(2025, 9, 15),
                EndDate = new DateTime(2025, 12, 21),
                UserId = testUser.Id
            };
            await database.InsertAsync(term);

            var insertedTerm = await database.Table<Term>()
                .Where(t => t.UserId == testUser.Id)
                .OrderByDescending(t => t.Id)
                .FirstAsync();
            var course = new Courses
            {
                TermId = insertedTerm.Id,
                UserId = testUser.Id,
                Name = "Mobile Application Development",
                StartDate = new DateTime(2025, 9, 15),
                EndDate = new DateTime(2025, 12, 21),
                Status = "In Progress",
                InstructorName = "Anika Patel",
                InstructorPhone = "5551234567",
                InstructorEmail = "anika.patel@strimeuniversity.edu"
            };
            await database.InsertAsync(course);

            var insertedCourse = await database.Table<Courses>()
                .Where(c => c.UserId == testUser.Id)
                .OrderByDescending(c => c.Id)
                .FirstAsync();
            await database.InsertAsync(new Assessments
            {
                CourseId = insertedCourse.Id,
                UserId = testUser.Id,
                Name = "Quiz",
                Type = "objective",
                StartDate = new DateTime(2025, 9, 15),
                EndDate = new DateTime(2025, 12, 21),
            });

            await database.InsertAsync(new Assessments
            {
                CourseId = insertedCourse.Id,
                UserId = testUser.Id,
                Name = "Final Project",
                Type = "performance",
                StartDate = new DateTime(2025, 9, 15),
                EndDate = new DateTime(2025, 12, 21),
            });

            await CreateSecondTestUser(database);
        }

        public static async Task CreateTestUser(SQLiteAsyncConnection database)
        {
            var existingUser = await database.Table<Users>()
                .Where(u => u.Username == "testuser")
                .FirstOrDefaultAsync();
            if (existingUser != null) return;

            var testUser = new Users
            {
                Username = "testuser",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                CreatedAt = DateTime.Now
            };
            await database.InsertAsync(testUser);
        }

        public static async Task CreateSecondTestUser(SQLiteAsyncConnection database)
        {
            var existingUser = await database.Table<Users>()
                .Where(u => u.Username == "johndoe")
                .FirstOrDefaultAsync();
            if (existingUser != null) return;

            var secondUser = new Users
            {
                Username = "johndoe",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password456"),
                CreatedAt = DateTime.Now
            };
            await database.InsertAsync(secondUser);

            var term = new Term
            {
                Name = "Spring 2025",
                StartDate = new DateTime(2025, 1, 15),
                EndDate = new DateTime(2025, 5, 15),
                UserId = secondUser.Id
            };
            await database.InsertAsync(term);

            var insertedTerm = await database.Table<Term>()
                .Where(t => t.UserId == secondUser.Id)
                .OrderByDescending(t => t.Id)
                .FirstAsync();

            var course = new Courses
            {
                TermId = insertedTerm.Id,
                UserId = secondUser.Id,
                Name = "Database Management",
                StartDate = new DateTime(2025, 1, 15),
                EndDate = new DateTime(2025, 5, 15),
                Status = "Completed",
                InstructorName = "Dr. Smith",
                InstructorPhone = "5559876543",
                InstructorEmail = "dr.smith@university.edu"
            };
            await database.InsertAsync(course);

            var insertedCourse = await database.Table<Courses>()
                .Where(c => c.UserId == secondUser.Id)
                .OrderByDescending(c => c.Id)
                .FirstAsync();

            await database.InsertAsync(new Assessments
            {
                CourseId = insertedCourse.Id,
                UserId = secondUser.Id,
                Name = "Midterm Exam",
                Type = "objective",
                StartDate = new DateTime(2025, 3, 1),
                EndDate = new DateTime(2025, 3, 1),
            });
        }
    }
}
