using System.Collections.Generic;
using UnityEngine;

public class SchoolClass : MonoBehaviour
{
    public string ClassName;
    public string Grade;

    // Klasse Students statt Student
    public List<Students> students = new List<Students>();
    public List<Teacher> teachers = new List<Teacher>();
    public List<Desk> desks = new List<Desk>();
    public List<Chair> chairs = new List<Chair>();

    public void AddStudent(Students s)
    {
        if (!students.Contains(s))
            students.Add(s);
    }

    public void AddTeacher(Teacher t)
    {
        if (!teachers.Contains(t))
            teachers.Add(t);
    }

    public void AddDesk(Desk d)
    {
        if (!desks.Contains(d))
            desks.Add(d);
    }

    public void AddChair(Chair c)
    {
        if (!chairs.Contains(c))
            chairs.Add(c);
    }
}
