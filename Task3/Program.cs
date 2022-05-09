using System;
using System.Reflection;
using System.Text.RegularExpressions;


namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = null;
            while (true)
            {
                try
                {
                    assembly = Assembly.LoadFrom(@"E:\Dropbox\dev\ITVDN\4 курс\HW6\Task3\bin\Release\net5.0\Task2");
                    // Почему не работает следующая строка?
                    //assembly = Assembly.Load("Task2");                    
                }
                catch (System.IO.FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }

                Console.WriteLine("Введите значение температуры с указанием шкалы иззмерения и требуемую шкалу для получения результата в сследующей формате:" +
                    "100K-C. Для выхода наберите q");
                string input = Console.ReadLine();
                if (input.ToLower() == "q") break;
                try
                {
                    double inputDouble = double.Parse(new Regex(@"^\d*").Match(input).Value);
                    char inputScale = new Regex(@"\D(?=-)").Match(input).Value.ToCharArray()[0];
                    char outputScale = new Regex(@"\D$").Match(input).Value.ToCharArray()[0];

                    Type enumSccale = assembly.GetType("Task2.Converter+TempScale");
                    object inputSccaleFormat = GetEnumScale(inputScale, enumSccale);
                    object outputSccaleFormat = GetEnumScale(outputScale, enumSccale);

                    dynamic converter = Activator.CreateInstance(assembly.GetType("Task2.Converter"), new object[] { inputDouble, inputSccaleFormat });
                    Console.WriteLine($"Температура {inputDouble}" +
                        $" в {inputSccaleFormat}" +
                        $" равна {GetTemp(converter, outputSccaleFormat)}" +
                        $" в {outputSccaleFormat}");
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                    {
                        Console.WriteLine(e.InnerException.Message);
                    }
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }
        private static object GetEnumScale(char inputScale, Type enumSccale)
        {
            if (inputScale == 'k' || inputScale == 'K' || inputScale == 'К' || inputScale == 'к')
            {
                return Enum.Parse(enumSccale, "Kelvin");
            }
            if (inputScale == 'F' || inputScale == 'f')
            {
                return Enum.Parse(enumSccale, "Fahrenheit");
            }
            if (inputScale == 'C' || inputScale == 'c' || inputScale == 'С' || inputScale == 'с')
            {
                return Enum.Parse(enumSccale, "Celsius");
            }
            return null;
        }
        private static string GetTemp(dynamic converter, object outputSccaleFormat)
        {
            if (outputSccaleFormat.ToString() == "Kelvin")
            {
                return converter.TempK.ToString();
            }
            if (outputSccaleFormat.ToString() == "Fahrenheit")
            {
                return converter.TempF.ToString();
            }
            if (outputSccaleFormat.ToString() == "Celsius")
            {
                return converter.TempC.ToString();
            }
            return "";
        }
    }
}
