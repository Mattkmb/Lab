using System;

namespace Project1
{
    public class DerivedClass : BaseClass
    {
        private int number;
        private bool isActive;

        // Конструктор по умолчанию
        public DerivedClass() : base()
        {
            number = 0;
            isActive = false;
        }

        // Конструктор с параметрами
        public DerivedClass(string text, int number, bool isActive) : base(text)
        {
            this.number = number;
            this.isActive = isActive;
        }

        // Конструктор копирования
        public DerivedClass(DerivedClass other) : base(other)
        {
            this.number = other.number;
            this.isActive = other.isActive;
        }

        // Метод для увеличения числа
        public void IncreaseNumber(int increment)
        {
            number += increment;
        }

        // Метод для переключения логического поля
        public void ToggleActive()
        {
            isActive = !isActive;
        }

        // Метод ввода дополнительных данных с проверкой
        public void InputAdditionalData()
        {
            // Ввод целого числа
            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();
            int num;
            while (!int.TryParse(input, out num))
            {
                Console.WriteLine("Ошибка ввода! Введите целое число.");
                Console.Write("Введите целое число: ");
                input = Console.ReadLine();
            }
            number = num;

            // Ввод булевого значения 
            Console.Write("Объект активен? (да/нет): ");
            input = Console.ReadLine()?.Trim().ToLower();
            while (input != "да" && input != "нет")
            {
                Console.WriteLine("Ошибка ввода! Введите 'да' или 'нет'.");
                Console.Write("Объект активен? (да/нет): ");
                input = Console.ReadLine()?.Trim().ToLower();
            }
            isActive = (input == "да");
        }

        // Переопределение метода ToString()
        public override string ToString()
        {
            return $"{base.ToString()}, Number: {number}, IsActive: {isActive}";
        }
    }
}