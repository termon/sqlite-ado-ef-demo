using System.Data.SQLite;
using Models;

namespace Repository;

public class SqliteAdo : ISqlite
{
    private readonly string connectionString; 
    
    public SqliteAdo(string connection) 
    {
        this.connectionString = connection;
    }

    public void DeleteAllStudents()
    {
        using var db = new SQLiteConnection(connectionString); 
        db.Open();
        using var cmd = db.CreateCommand();
        cmd.CommandText = @"DELETE FROM students";           
        cmd.ExecuteNonQuery();
        db.Close();
    }

    public bool DeleteStudent(int id)
    {
        using var db = new SQLiteConnection(connectionString); 
        db.Open();
        using var cmd = db.CreateCommand();
        cmd.CommandText = @"DELETE FROM students WHERE id=$id";  
        cmd.Parameters.AddWithValue("$id", id);
        var r = cmd.ExecuteNonQuery();                 
        db.Close();
        return r == 1;
    }

    public List<Student> GetAllStudents()
    {
        using var db = new SQLiteConnection(connectionString); 
        
        db.Open();
    
        using var cmd = db.CreateCommand();   
        cmd.CommandText = @"SELECT Id, Name FROM students";
        var reader = cmd.ExecuteReader();
        var results = new List<Student>();
        while (reader.Read()) {
            results.Add(new Student { Id = reader.GetInt32(0),  Name = reader.GetString(1) });
        }
        return results;
    }

    public Student GetStudentById(int id)
    {
        using var db = new SQLiteConnection(connectionString); 
        db.Open();

        var results = new List<Student>();
        using var cmd = db.CreateCommand();
        cmd.Parameters.AddWithValue("$id", id);
        cmd.CommandText = @"SELECT Id, Name FROM students WHERE id=$id LIMIT 1";
        using var reader = cmd.ExecuteReader();
        Student result = null;
        if (reader.Read()) {
            result = new Student { Id = reader.GetInt32(0),  Name = reader.GetString(1) };
        }
        db.Close();
        return result;
    }

    public Student CreateStudent(int id, string name)
    {
        using var db = new SQLiteConnection(connectionString);         
        db.Open();

        using var cmd = db.CreateCommand();
        cmd.CommandText = "INSERT INTO students(id, name) VALUES($id,$name)";
        cmd.Parameters.AddWithValue("$id", id);
        cmd.Parameters.AddWithValue("$name", name);
        cmd.Prepare();
        cmd.ExecuteNonQuery();       
        
        db.Close();

        return GetStudentById(id);
    }

    public Student CreateStudent(string name)
    {
        using var db = new SQLiteConnection(connectionString); 
        db.Open();
        
        using var cmd = db.CreateCommand();
        cmd.CommandText = "INSERT INTO students(name) VALUES($name)"; 
        cmd.Parameters.AddWithValue("$name", name);
        cmd.Prepare();
        cmd.ExecuteNonQuery();

        var id = (int)db.LastInsertRowId;
        db.Close();
        return GetStudentById(id);
    }
    
    public void CreateDatabase()
    {
        using var db = new SQLiteConnection(connectionString);
        db.Open();
        
        using var cmd = db.CreateCommand();
        cmd.CommandText =  "DROP TABLE IF EXISTS Students";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Students(id INTEGER PRIMARY KEY, name TEXT)";
        cmd.ExecuteNonQuery();
        
        db.Close();
    }
    
}