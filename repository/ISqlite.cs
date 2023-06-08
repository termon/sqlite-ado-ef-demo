
using Models;

namespace Repository;

public interface ISqlite 
{

    public void CreateDatabase();
    
    public void DeleteAllStudents();

    public bool DeleteStudent(int id);
    
    public List<Student> GetAllStudents();

    public Student GetStudentById(int id);

    public Student CreateStudent(int id, string name);
    public Student CreateStudent(string name);       

}