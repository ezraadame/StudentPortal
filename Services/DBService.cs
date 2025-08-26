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

                await _db.CreateTableAsync<Term>();
                await _db.CreateTableAsync<Courses>();
                await _db.CreateTableAsync<Assessments>();
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
            await _db!.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Courses>();
            await _db.CreateTableAsync<Assessments>();
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
    }
}