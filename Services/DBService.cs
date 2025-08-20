using SQLite;
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
            //Absolute path to database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "StudentPortal_db.db");

            _db = new SQLiteAsyncConnection(databasePath);

            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Courses>();
            await _db.CreateTableAsync<Assessments>();

            //TODO Create Users section of app: await _db.CreateTableAsync<Users>();

        }
        public static async Task CreateTables()
        {
            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Courses>();
            await _db.CreateTableAsync<Assessments>();
        }
        public static async Task InsertTerm(Term term)
        {
            await Init();
            await _db.InsertAsync(term);
        }

        public static async Task EditTerms(Term term)
        {
            await Init();
            await _db.UpdateAsync(term);
        }

        public static async Task DeleteTerm(Term term)
        {
            await Init();
            await _db.DeleteAsync(term);
        }

        public static async Task<List<Term>> GetTerms()
        {
            await Init();
            return await _db.Table<Term>().ToListAsync();
        }

        //

        public static async Task InsertCourse(Courses course)
        {
            await Init();
            await _db.InsertAsync(course);
        }
        public static async Task DeleteCourse(Courses course)
        {
            await Init();
            await _db.DeleteAsync(course);
        }
        public static async Task<List<Courses>> GetCourses()
        {
            await Init();
            return await _db.Table<Courses>().ToListAsync();
        }
    }
}