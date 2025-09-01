
using Microsoft.Maui.Controls.Compatibility;
using SQLite;
using StudentPortal.EvaluationData;
using StudentPortal.Models;
using System.IO;

namespace StudentPortal.Services
{
    public static class DBService
    {
        private static SQLiteAsyncConnection? _db;
        public static async Task Init()
        {
            if (_db != null)
            {
                return;
            }

            try
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "StudentPortal_db.db");
                _db = new SQLiteAsyncConnection(databasePath);

                await _db.CreateTableAsync<Users>();
                await _db.CreateTableAsync<Term>();
                await _db.CreateTableAsync<Courses>();
                await _db.CreateTableAsync<Assessments>();
                await _db.CreateTableAsync<Reports>();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize database", ex);
            }
        }
        private static void EnsureDatabaseInitialized()
        {
            if (_db == null)
                throw new InvalidOperationException("Database is not initialized.");
        }
        public static async Task CreateTables()
        {
            EnsureDatabaseInitialized();
            await _db.CreateTableAsync<Users>();
            await _db!.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Courses>();
            await _db.CreateTableAsync<Assessments>();
            await _db.CreateTableAsync<Reports>();
        }

        public static async Task InsertTerm(Term term)
        {
            await Init();
            EnsureDatabaseInitialized();
            await _db!.InsertAsync(term);
        }

        public static async Task EditTerms(Term term)
        {
            await Init();
            EnsureDatabaseInitialized();
            await _db!.UpdateAsync(term);
        }

        public static async Task DeleteTerm(Term term)
        {
            await Init();
            EnsureDatabaseInitialized();
            await _db!.DeleteAsync(term);
        }

        public static async Task<List<Term>> GetTerms()
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Term>().ToListAsync();
        }

        public static async Task InsertCourse(Courses course)
        {
            await Init();
            EnsureDatabaseInitialized();
            await _db!.InsertAsync(course);
        }

        public static async Task EditCourse(Courses course)
        {
            await Init();
            EnsureDatabaseInitialized();
            await _db!.UpdateAsync(course);
        }

        public static async Task DeleteCourse(Courses course)
        {
            await Init();
            EnsureDatabaseInitialized();
            await _db!.DeleteAsync(course);
        }

        public static async Task<List<Courses>> GetCourses()
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Courses>().ToListAsync();
        }

        public static async Task<Courses> GetCourse(int courseId)
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.FindAsync<Courses>(courseId);
        }

        public static async Task<List<Courses>> GetCoursesByTerm(int termId)
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Courses>().Where(course => course.TermId == termId).ToListAsync();
        }

        public static async Task<List<Assessments>> GetAssessmentsByCourse(int courseId)
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Assessments>().Where(assessment => assessment.CourseId == courseId).ToListAsync();
        }

        public static async Task<List<Assessments>> GetAssessments()
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Assessments>().ToListAsync();
        }

        public static async Task InsertAssessment(Assessments assessment)
        {
            await Init();
            EnsureDatabaseInitialized();
            await _db!.InsertAsync(assessment);
        }

        public static async Task EditAssessment(Assessments assessments)
        {
            await Init();
            EnsureDatabaseInitialized();
            await _db!.UpdateAsync(assessments);
        }

        public static async Task DeleteAssessment(Assessments assessments)
        {
            await Init();
            EnsureDatabaseInitialized();
            await _db!.DeleteAsync(assessments);
        }

        public static async Task InitializeEvaluationData()
        {
            await Init();
            EnsureDatabaseInitialized();
            await AnikaPatelData.CreateEvaluationData(_db!);
        }
        public static async Task<Users> GetUserByUsername(string username)
        {
            await Init();
            EnsureDatabaseInitialized();
            return await 
                _db.Table<Users>()       
                .Where(u => u.Username == username)             
                .FirstOrDefaultAsync();
        }
        public static async Task<bool> ValidateUser(string username, string password)
        {
            var user = await GetUserByUsername(username);

            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public static async Task<bool> UserExists(string username)
        {
            var user = await GetUserByUsername(username);
            return user != null;
        }

        public static async Task<List<Reports>> GenerateAssessmentReport()
        {
            await Init();
            EnsureDatabaseInitialized();

            var reportItems = new List<Reports>();
            var courses = await _db.Table<Courses>().ToListAsync();

            foreach (var course in courses)
            {
                var assessments = await _db.Table<Assessments>()
                    .Where(a => a.CourseId == course.Id)
                    .ToListAsync();

                foreach (var assessment in assessments)
                {
                    reportItems.Add(new Reports
                    {
                        CourseName = course.Name,
                        AssessmentName = assessment.Name,
                        Type = assessment.Type,
                        StartDate = assessment.StartDate,
                        EndDate = assessment.EndDate,

                    });
                }
            }

            return reportItems;
        }
        public static async Task<List<Term>> GetTermsByUser(int userId)
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Term>()
                .Where(term => term.UserId == userId)
                .ToListAsync();
        }

        public static async Task<List<Courses>> GetCoursesByUser(int userId)
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Courses>()
                .Where(course => course.UserId == userId)
                .ToListAsync();
        }

        public static async Task<List<Courses>> GetCoursesByTermAndUser(int termId, int userId)
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Courses>()
                .Where(course => course.TermId == termId && course.UserId == userId)
                .ToListAsync();
        }
        public static async Task<List<Assessments>> GetAssessmentsByUser(int userId)
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Assessments>()
                .Where(assessment => assessment.UserId == userId)
                .ToListAsync();
        }

        public static async Task<List<Assessments>> GetAssessmentsByCourseAndUser(int courseId, int userId)
        {
            await Init();
            EnsureDatabaseInitialized();
            return await _db!.Table<Assessments>()
                .Where(assessment => assessment.CourseId == courseId && assessment.UserId == userId)
                .ToListAsync();
        }
        public static async Task<List<Reports>> GenerateAssessmentReportForUser(int userId)
        {
            await Init();
            EnsureDatabaseInitialized();

            var reportItems = new List<Reports>();
            var courses = await _db.Table<Courses>()
                .Where(c => c.UserId == userId)
                .ToListAsync();

            foreach (var course in courses)
            {
                var assessments = await _db.Table<Assessments>()
                    .Where(a => a.CourseId == course.Id && a.UserId == userId)
                    .ToListAsync();

                foreach (var assessment in assessments)
                {
                    reportItems.Add(new Reports
                    {
                        CourseName = course.Name,
                        AssessmentName = assessment.Name,
                        Type = assessment.Type,
                        StartDate = assessment.StartDate,
                        EndDate = assessment.EndDate,
                    });
                }
            }
            return reportItems;
        }


    }
}