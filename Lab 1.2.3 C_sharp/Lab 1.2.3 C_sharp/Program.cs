using System;

namespace Project2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----- Тестирование Project2: Задания 2 и 3  -----");

            // Создание объекта и ввод данных
            Money m1 = new Money();
            Console.WriteLine("Введите денежную сумму:");
            m1.InputMoney();
            Console.WriteLine("Начальное значение: " + m1);

            // добавление копеек
            Console.Write("Введите количество копеек для добавления: ");
            string input = Console.ReadLine();
            uint addKopeks;
            while (!uint.TryParse(input, out addKopeks))
            {
                Console.WriteLine("Ошибка ввода! Введите корректное целое число.");
                Console.Write("Введите количество копеек для добавления: ");
                input = Console.ReadLine();
            }
            Money m2 = m1 + addKopeks;
            Console.WriteLine("После добавления копеек (оператор +): " + m2);

            // вычитание копеек
            Console.Write("Введите количество копеек для вычитания: ");
            input = Console.ReadLine();
            uint subKopeks;
            while (!uint.TryParse(input, out subKopeks))
            {
                Console.WriteLine("Ошибка ввода! Введите корректное целое число.");
                Console.Write("Введите количество копеек для вычитания: ");
                input = Console.ReadLine();
            }
            Money m3 = m2 - subKopeks;
            Console.WriteLine("После вычитания копеек (оператор –): " + m3);

            // операторы ++ и --
            Money m4 = new Money(m3);
            Console.WriteLine("Применяем оператор ++:");
            m4++;
            Console.WriteLine("После ++: " + m4);
            Console.WriteLine("Применяем оператор --:");
            m4--;
            Console.WriteLine("После --: " + m4);

            // преобразовани типов
            uint rubles = (uint)m4;    
            double fraction = m4;      
            Console.WriteLine("Явное преобразование в uint (рубли): " + rubles);
            Console.WriteLine("Неявное преобразование в double (копейки, переведённые в рубли): " + fraction);

        }
    }
}
