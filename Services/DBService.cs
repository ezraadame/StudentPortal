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

            //Users
            //TODO await _db.CreateTableAsync<Users>();

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
    }
}