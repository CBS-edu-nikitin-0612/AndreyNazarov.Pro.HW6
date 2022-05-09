using System;
using Task2;
using System.Reflection;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(typeof(Task2.Converter).Assembly.FullName);
        }
    }
}
