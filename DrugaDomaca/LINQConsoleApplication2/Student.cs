using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQConsoleApplication2
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public static bool operator ==(Student a, Student b) { return a.Equals(b); }
        public static bool operator !=(Student a, Student b) { return !a.Equals(b); }
        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;         
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Student s = (Student) obj;
            return Jmbag == s.Jmbag;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Jmbag.GetHashCode();
        }
    }

    public enum Gender
    {
        Male, Female
    }

   
}
