using SQLite;

namespace StudentPortal.Models
{
    public class Courses
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}