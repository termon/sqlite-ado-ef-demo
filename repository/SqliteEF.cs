using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

using Models;

namespace Repository;

public class SqliteEF : ISqlite
{

    private readonly Database db;
    private readonly string connectionString; 

    private class Database : DbContext
    {
        private readonly string connectionString; 
        public DbSet<Student> Students { get; set;}

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder  
                .UseSqlite(connectionString)                            
                //.LogTo(Console.WriteLine, LogLevel.Information) // remove in production
                //.EnableSensitiveDataLogging()                   // remove in production   
                ;             
        }

        public void CreateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }

    public SqliteEF(string connection) 
    {
        this.connectionString = connection;
        db = new Database(this.connectionString);
    }


    public void DeleteAllStudents()
    {
        using var db = new Database(this.connectionString);
        var students = db.Students.AsNoTracking();
        db.RemoveRange(students);
        db.SaveChanges();
    }

    public bool DeleteStudent(int id)
    {
        using var db = new Database(this.connectionString);
        var student = db.Students.FirstOrDefault(s => s.Id == id);
        var r = db.Remove(student);
        db.SaveChanges();
        return r != null;
    }
    public List<Student> GetAllStudents()
    {
        using var db = new Database(this.connectionString);
        return db.Students.AsNoTracking().AsNoTracking().ToList();
    }

    public Student GetStudentById(int id)
    {
        using var db = new Database(this.connectionString);
        return db.Students.FirstOrDefault(s => s.Id == id);
    }

    public Student CreateStudent(int id, string name)
    {
        using var db = new Database(this.connectionString);
        var student = new Student { Id = id, Name = name };
        db.Add(student);
        db.SaveChanges();
        return student;
    }
    
    public Student CreateStudent(string name)
    {
        using var db = new Database(this.connectionString);
        var student = new Student { Name = name };
        db.Add(student);
        db.SaveChanges();
        return student;
    }

    public void CreateDatabase()
    {
        db.CreateDatabase();
    }
}