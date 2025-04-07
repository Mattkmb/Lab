using System;

namespace Project1
{
    public class BaseClass
    {
        // Строковое поле
        protected string text;

        // Конструктор по умолчанию
        public BaseClass()
        {
            text = string.Empty;
        }

        // Конструктор с параметром
        public BaseClass(string text)
        {
            this.text = text;
        }

        // Конструктор копирования
        public BaseClass(BaseClass other)
        {
            this.text = other.text;
        }

        // Метод, приписывающий к полю в начало три знака восклицания
        public void PrependExclamations()
        {
            text = "!!!" + text;
        }

        // Метод ввода с клавиатуры с проверкой 
        public void InputText()
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка! Строка не должна быть пустой.");
                Console.Write("Введите строку: ");
                input = Console.ReadLine();
            }
            text = input;
        }

        // Переопределение ToString() 
        public override string ToString()
        {
            return $"Text: {text}";
        }
    }
}