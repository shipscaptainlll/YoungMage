using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqCache : MonoBehaviour
{
    List<Student> students = new List<Student>();
    List<People> people = new List<People>();
    List<People> peopleSecond = new List<People>();
    List<People> peopleThird = new List<People>();

    // Start is called before the first frame update
    void Start()
    {
        /*
        AddStudent(12, "Maxim", people);
        AddTeacher(42, "MrSergei", people);
        AddTeacher(52, "LrMalkolm", people);
        AddTeacher(22, "Masha", people);
        AddTeacher(22, "Valeri", people);
        AddTeacher(32, "MrNikita", people);

        AddTeacher(12, "SecondSergei", peopleSecond);
        AddTeacher(22, "SecondNik", peopleSecond);
        AddTeacher(22, "SecondNik", peopleSecond);
        AddTeacher(32, "SecondNik", peopleSecond);

        var sortedPeople = from s in people
                           where s.age > 30 && s.age < 60
                           select s
                           into teenStudents
                           where teenStudents.name.StartsWith("M")
                           select teenStudents;

        var sortedSecondPeople = peopleSecond.Select(s => s.age).Distinct();
        var concatedSeque = Enumerable.Repeat(new Teacher(10, "Malcom"), 10);


        foreach (var element in sortedPeople)
        {
            Debug.Log(element.age + " age " + element.name + " name");
        }
        */
    }

    void AddStudent(int age, string name, List<People> targetList)
    {
        Student student = new Student(age, name);
        targetList.Add(student);
    }

    void AddTeacher(int age, string name, List<People> targetList)
    {
        Teacher teacher = new Teacher(age, name);
        targetList.Add(teacher);
    }
}

public class Student : People
{

    public Student(int age, string name)
    {
        this.age = age;
        this.name = name;
    }
}

public class Teacher : People
{

    public Teacher(int age, string name)
    {
        this.age = age;
        this.name = name;
    }
}

public abstract class People : Object
{
    public int age;
    public string name;
}
