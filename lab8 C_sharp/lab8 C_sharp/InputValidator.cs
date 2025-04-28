using System;
using System.Globalization;

namespace MusicCatalog
{
    /// <summary>
    /// Класс для безопасного чтения и проверки входных данных из консоли.
    /// </summary>
    public static class InputValidator
    {
        /// <summary>Читает непустую строку.</summary>
        public static string ReadString(string prompt)
        {
            string s;
            do
            {
                Console.Write(prompt);
                s = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(s))
                    Console.WriteLine("Значение не может быть пустым.");
            } while (string.IsNullOrEmpty(s));
            return s;
        }

        /// <summary>Читает целое число.</summary>
        public static int ReadInt(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Неверный ввод. Введите целое число: ");
            }
            return value;
        }

        /// <summary>Читает дату в формате dd-MM-yyyy.</summary>
        public static DateTime ReadDate(string prompt)
        {
            DateTime dt;
            Console.Write(prompt);
            while (!DateTime.TryParseExact(
                       Console.ReadLine(),
                       "dd-MM-yyyy",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out dt))
            {
                Console.Write("Неверный формат. Ожидается дд-MM-yyyy: ");
            }
            return dt;
        }

        /// <summary>Читает длительность в формате mm:ss.</summary>
        public static TimeSpan ReadTimeSpan(string prompt)
        {
            TimeSpan ts;
            Console.Write(prompt);
            while (!TimeSpan.TryParseExact(
                       Console.ReadLine(),
                       "m\\:ss",
                       CultureInfo.InvariantCulture,
                       out ts))
            {
                Console.Write("Неверный формат. Ожидается mm:ss: ");
            }
            return ts;
        }

        /// <summary>Читает неотрицательное десятичное число.</summary>
        public static decimal ReadDecimal(string prompt)
        {
            decimal d;
            Console.Write(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out d) || d < 0)
            {
                Console.Write("Неверный ввод. Введите неотрицательное число: ");
            }
            return d;
        }
    }
}
