using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using StudentPortal.Services;

namespace StudentPortal.Models
{
    [Table("Users")]
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
