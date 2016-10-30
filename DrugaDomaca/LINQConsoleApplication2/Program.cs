using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
            Example2();
            Console.ReadLine();
        }

        static void Example1()
        {
            var list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 ")
            };
            var ivan = new Student(" Ivan ", jmbag: " 001234567 ");
            // false :(
            bool anyIvanExists = list.Any(i => i == ivan);
            Console.WriteLine(anyIvanExists ? "Postoji Ivan." : "Ne postoji Ivan.");
        }
        static void Example2()
        {
            var list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 ") ,
                new Student (" Ivan ", jmbag :" 001234567 ")
            };
            // 2 :(
            var distinctStudents = list.Distinct().Count();
            Console.WriteLine(distinctStudents + " -> "  
                + (distinctStudents == 1 ? "Ne brojim iste." : "Brojim iste."));
        }
    }
}
