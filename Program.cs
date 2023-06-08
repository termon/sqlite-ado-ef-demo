
using Repository;


ISqlite db = new SqliteAdo("Data Source=sms.db"); 
//ISqlite db = new SqliteEF("Data Source=sms.db");

db.CreateDatabase();

Console.WriteLine("Creating Students...");
var s1 = db.CreateStudent(1, "Homer Simpson");
var s2 = db.CreateStudent(2, "Marge Simpson");
var s3 = db.CreateStudent(3, "Bart Simpson");
var s4 = db.CreateStudent(4, "Lisa Simpson");
var s5 = db.CreateStudent(5, "Maggie Simpson");
var s6 = db.CreateStudent(6, "Mr Burns");

Console.WriteLine("Retrieving Students...");
var students = db.GetAllStudents();
students.ForEach(s => Console.WriteLine(s));

if (db.DeleteStudent(s1.Id))
{
    Console.WriteLine($"Student {s1.Id} deleted..");
}
else
{ 
    Console.WriteLine($"Student {s1.Id} NOT deleted..");
}

Console.Write($"Retrieving Student {s1.Id}...");
var result = db.GetStudentById(1);
if (result != null) 
{
    Console.WriteLine(result);
}
else
{
        Console.WriteLine("Not found");
}

Console.WriteLine("Deleting all Students...");
db.DeleteAllStudents();
