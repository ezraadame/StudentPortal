using SQLite;
using StudentPortal.Services;


namespace StudentPortal.Models;

[Table("Terms")]
public class Term
{
   
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public static async Task AddTerm(int id, string? name, DateTime startDate, DateTime endDate)
    {
        await DBService.Init();
        var term = new Term()
        {
            Id = id,
            Name = name,
            StartDate = startDate, 
            EndDate = endDate
        };
        await DBService.InsertTerm(term);
    }

    public static async Task EditTerm(int id, string? name, DateTime startDate, DateTime endDate)
    {
        await DBService.Init();
        var term = new Term()
        {
            Id = id,
            Name = name,
            StartDate = startDate,
            EndDate = endDate
        };
        await DBService.EditTerms(term);
    }
}
