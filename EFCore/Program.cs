// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using EFCore;

using (StudentContext db = new StudentContext())
{
    db.Add(new Student() { FullName = "Ivan", Age = 25 });
    db.SaveChanges();

    List<Student> students = db.Students.ToList();

    foreach(Student student in students)
    {
        Console.WriteLine($"{student.Id}. {student.FullName}, {student.Age}");
    }
}