using SQLite;

namespace StudentPortal.Services
{
    public class Assessments
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}