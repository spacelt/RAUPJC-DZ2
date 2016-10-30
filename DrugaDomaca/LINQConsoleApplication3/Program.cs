using LINQConsoleApplication2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            University[] universities = GetAllCroatianUniversities();

            Student[] allCroatianStudents = universities.SelectMany(i => i.Students)
                                                        .Distinct()
                                                        .ToArray();
            Student[] croatianStudentsOnMultipleUniversities = universities.SelectMany(i => i.Students).GroupBy(s => s)
                                                                           .Where(group => group.Count() > 1)
                                                                           .Select(group => group.Key)
                                                                           .ToArray();
            Student[] studentsOnMaleOnlyUniversities = universities.Where(i => i.Students.All(s => s.Gender == Gender.Male))
                                                                   .SelectMany(i => i.Students)
                                                                   .Distinct()
                                                                   .ToArray();

            Console.WriteLine("All Croatian students:");
            printStudents(allCroatianStudents);
            Console.WriteLine("\nStudents on multiple universities: ");
            printStudents(croatianStudentsOnMultipleUniversities);
            Console.WriteLine("\nStudents on male only universities: ");
            printStudents(studentsOnMaleOnlyUniversities);

            Console.ReadLine();
        }

        private static University[] GetAllCroatianUniversities()
        {
            University u1 = new University();
            u1.Name = "Zagreb";
            University u2 = new University();
            u1.Name = "Osijek";
            University u3 = new University();
            u1.Name = "Rijeka";
            University u4 = new University();
            u1.Name = "Split";
            University u5 = new University();
            u1.Name = "Dubrovnik";

            Student s1 = new Student("Frane", "0036124658");
            s1.Gender = Gender.Male;
            Student s2 = new Student("Ivan", "0036124651");
            s2.Gender = Gender.Male;
            Student s3 = new Student("Marko", "0036124652");
            s3.Gender = Gender.Male;
            Student s4 = new Student("Željko", "0036124653");
            s4.Gender = Gender.Male;
            Student s5 = new Student("Petar", "0036124654");
            s5.Gender = Gender.Male;
            Student s6 = new Student("Josip", "0036124655");
            s6.Gender = Gender.Male;
            Student s7 = new Student("Josipa", "0036124632");
            s7.Gender = Gender.Female;
            Student s8 = new Student("Lucija", "0036124634");
            s8.Gender = Gender.Female;
            Student s9 = new Student("Franka", "0036124665");
            s9.Gender = Gender.Female;
            Student s10 = new Student("Ivana", "0036124688");
            s10.Gender = Gender.Female;

            u1.Students = new Student[] { s1, s2, s3, s4 };
            u2.Students = new Student[] { s4, s5 };
            u3.Students = new Student[] { s6, s7 };
            u4.Students = new Student[] { s8, s9, s10 };
            u5.Students = new Student[] { s1, s9, s10 };

            return new University[] { u1, u2, u3, u4, u5 };
        }

        private static void printStudents(Student[] students)
        {
            foreach (Student s in students)
            {
                Console.WriteLine(s.Jmbag + " | " + s.Name + " | " + s.Gender);
            }
        }
    }
}
