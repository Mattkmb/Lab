using System;

namespace Lab7Project
{
    public static class InputHandler
    {
        public static int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result))
                    return result;
                Console.WriteLine("Ошибка: введите целое число.");
            }
        }

        public static double ReadDouble(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (double.TryParse(input, out result))
                    return result;
                Console.WriteLine("Ошибка: введите число с плавающей точкой.");
            }
        }

        public static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine("Ошибка: строка не должна быть пустой.");
            }
        }
    }
}
