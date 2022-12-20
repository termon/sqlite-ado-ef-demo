
public interface ISqliteDb 
{

    public void CreateDatabase();
    
    public void deleteAllStudents();

    public bool deleteStudent(int id);
    
    public List<Student> getAllStudents();

    public Student getStudentById(int id);

    public Student createStudent(int id, string name);
    public Student createStudent(string name);       

}