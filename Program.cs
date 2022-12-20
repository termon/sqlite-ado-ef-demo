
ISqliteDb db = new SqliteAdo("Data Source=sms.db"); 
//ISqliteDb db = new SqliteEF("Data Source=sms.db");

db.CreateDatabase();

Console.WriteLine("Creating Students...");
var s1 = db.createStudent(1, "Homer Simpson");
var s2 = db.createStudent(2, "Marge Simpson");
var s3 = db.createStudent(3, "Bart Simpson");
var s4 = db.createStudent(4, "Lisa Simpson");
var s5 = db.createStudent(5, "Maggie Simpson");
var s6 = db.createStudent(6, "Mr Burns");

Console.WriteLine("Retrieving Students...");
var students = db.getAllStudents();
students.ForEach(s => Console.WriteLine(s));

if (db.deleteStudent(s1.Id))
{
    Console.WriteLine($"Student {s1.Id} deleted..");
}
else
{ 
    Console.WriteLine($"Student {s1.Id} NOT deleted..");
}

Console.Write($"Retrieving Student {s1.Id}...");
var result = db.getStudentById(1);
if (result != null) 
{
    Console.WriteLine(result);
}
else
{
        Console.WriteLine("Not found");
}

Console.WriteLine("Deleting all Students...");
db.deleteAllStudents();
