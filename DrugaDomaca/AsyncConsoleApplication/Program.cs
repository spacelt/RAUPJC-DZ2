using System;
using System.Threading.Tasks;

namespace AsyncConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }
        private static async void LetsSayUserClickedAButtonOnGuiMethod()
        {
            var res = await GetTheMagicNumber();
            Console.WriteLine(res);
        }

        private static async Task<int> GetTheMagicNumber()
        {
            var res = await IKnowIGuyWhoKnowsAGuy();
            return res;
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            var res1 = await IKnowWhoKnowsThis(10);
            var res2 = await IKnowWhoKnowsThis(5);
            return res1 + res2;
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            var result = await FactorialDigitSum(n);
            return result;
        }
        public static async Task<int> FactorialDigitSum(int n)
        {
            Task<int> t = new Task<int>(() => {
                int fact = Factorial(n);
                int digitSum = 0;
                do
                {
                    digitSum += fact % 10;
                } while ((fact /= 10) > 0);
                return digitSum;
            }
            );
            t.Start();
            return await t;

        }

        private static int Factorial(int n)
        {
            return n == 1 ? 1 : n * Factorial(n - 1);
        }

    }
}
