using System;

namespace Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----- Тестирование Project1: Задание 1  -----");

            // Тестирование базового класса
            BaseClass baseObj = new BaseClass();
            baseObj.InputText();
            Console.WriteLine("Базовый класс до применения метода: " + baseObj);
            baseObj.PrependExclamations();
            Console.WriteLine("Базовый класс после применения метода: " + baseObj);

            // Тест конструктора копирования для базового класса
            BaseClass copyBase = new BaseClass(baseObj);
            Console.WriteLine("Копия базового класса: " + copyBase);

            // Тестирование дочернего класса
            DerivedClass derivedObj = new DerivedClass();
            Console.WriteLine("\n--- Ввод данных для дочернего класса ---");
            derivedObj.InputText();
            derivedObj.InputAdditionalData();
            Console.WriteLine("Дочерний класс до изменений: " + derivedObj);
            derivedObj.PrependExclamations();
            Console.WriteLine("Дочерний класс после применения метода PrependExclamations: " + derivedObj);
            derivedObj.IncreaseNumber(5);
            derivedObj.ToggleActive();
            Console.WriteLine("Дочерний класс после дополнительных изменений: " + derivedObj);

            // Тест конструктора копирования для дочернего класса
            DerivedClass copyDerived = new DerivedClass(derivedObj);
            Console.WriteLine("Копия дочернего класса: " + copyDerived);
        }
    }
}
