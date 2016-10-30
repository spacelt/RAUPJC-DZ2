using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            var result = integers.GroupBy(i => i)
                                 .Select(group => $"Broj {group.Key} pojavljuje se {group.Count()} puta")
                                 .ToArray();
            string[] strings = result;
            foreach(string s in strings) {
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }
    }
}
