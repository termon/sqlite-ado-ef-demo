using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

using models;

namespace repository;

public class SqliteEF : ISqlite
{

    private Database db;
    private string ConnectionString; 

    private class Database : DbContext
    {
        public DbSet<Student> Students { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder  
                .UseSqlite("Data Source=sms.db")                            
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
        this.ConnectionString = connection;
        db = new Database();
    }


    public void deleteAllStudents()
    {
        using (var db = new Database()) 
        {   
            var students = db.Students.AsNoTracking();
            db.RemoveRange(students);
            db.SaveChanges();
        }
    }

    public bool deleteStudent(int id)
    {
        using (var db = new Database()) 
        {   
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            var r = db.Remove(student);
            db.SaveChanges();
            return r != null;
        }
    }
    public List<Student> getAllStudents()
    {
        using (var db = new Database()) 
        { 
            return db.Students.AsNoTracking().AsNoTracking().ToList();
        }
    }

    public Student getStudentById(int id)
    {
        using (var db = new Database()) 
        { 
            return db.Students.FirstOrDefault(s => s.Id == id);
        }
    }

    public Student createStudent(int id, string name)
    {
        using (var db = new Database()) 
        { 
            var student =  new Student { Id = id, Name = name} ;                                       
            db.Add(student);
            db.SaveChanges();
            return student;
        }
    }
    
    public Student createStudent(string name)
    {
        using (var db = new Database()) 
        { 
            var student = new Student { Name = name}  ;
            db.Add(student);
            db.SaveChanges();
            return student;
        }
    }

    public void CreateDatabase()
    {
        db.CreateDatabase();
    }
}