using System;
using System.Collections.Generic;
using System.IO;

namespace Lab7Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ЗАДАНИЕ 1. Текстовый файл: найти сумму квадратов элементов.
            Console.WriteLine("=== ЗАДАНИЕ 1 ===");
            string task1File = "task1.txt";
            Lab7.FillTextFileTask1(task1File, 10); // файл с 10 целыми числами
            Lab7.Task1(task1File);

            // ЗАДАНИЕ 2. Текстовый файл: вычислить произведение элементов.
            Console.WriteLine("\n=== ЗАДАНИЕ 2 ===");
            string task2File = "task2.txt";
            Lab7.FillTextFileTask2(task2File, 5, 4); // 5 строк по 4 числа в каждой
            Lab7.Task2(task2File);

            // ЗАДАНИЕ 3. Текстовый файл: переписать в другой файл строки, имеющие заданную длину m.
            Console.WriteLine("\n=== ЗАДАНИЕ 3 ===");
            string task3Input = "task3_input.txt";
            string task3Output = "task3_output.txt";
            Lab7.FillTextFileTask3(task3Input); 
            int m = 3; // длина строки = 5 символов
            Lab7.Task3(task3Input, task3Output, m);
            Console.WriteLine("Содержимое выходного файла задачи 3:");
            foreach (string line in File.ReadAllLines(task3Output))
            {
                Console.WriteLine(line);
            }

            // ЗАДАНИЕ 4. Бинарный файл: вычислить произведение нечетных отрицательных компонент файла.
            Console.WriteLine("\n=== ЗАДАНИЕ 4 ===");
            string task4File = "task4.bin";
            Lab7.FillBinaryFileTask4(task4File, 20);
            Lab7.Task4(task4File);

            // ЗАДАНИЕ 5. Бинарные файлы и структуры (XML): обработка информации о багаже пассажира.
            Console.WriteLine("\n=== ЗАДАНИЕ 5 ===");
            string task5File = "task5.xml";
            Lab7.FillXMLFileTask5(task5File, 5, 1, 4); // 5 пассажиров, от 1 до 4 единиц багажа у каждого
            Lab7.Task5(task5File);

            // ЗАДАНИЕ 6. List: заменить первое вхождение списка L1 в L на содержимое L2.
            Console.WriteLine("\n=== ЗАДАНИЕ 6 ===");
            List<int> L = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> L1 = new List<int> { 4, 5, 6 };
            List<int> L2 = new List<int> { 40, 50 };
            Lab7.Task6_List(L, L1, L2);

            // ЗАДАНИЕ 7. LinkedList: сортировать элементы списка по возрастанию.
            Console.WriteLine("\n=== ЗАДАНИЕ 7 ===");
            LinkedList<int> linkedList = new LinkedList<int>(new int[] { 5, 2, 9, 1, 7 });
            Lab7.Task7_LinkedList(linkedList);

            // ЗАДАНИЕ 8. HashSet (Компьютерные игры):
            Console.WriteLine("\n=== ЗАДАНИЕ 8 ===");
            List<string> masterGames = new List<string> { "CS", "Dota", "Fortnite", "Minecraft", "Overwatch" };
            
            List<HashSet<string>> studentGames = new List<HashSet<string>>
            {
                new HashSet<string> { "CS", "Dota" },
                new HashSet<string> { "CS", "Fortnite", "Overwatch" },
                new HashSet<string> { "CS", "Dota", "Fortnite" }
            };
            Lab7.Task8_HashSet(masterGames, studentGames);

            // ЗАДАНИЕ 9. HashSet (Глухие согласные):
            Console.WriteLine("\n=== ЗАДАНИЕ 9 ===");
            string task9File = "task9.txt";
            using (StreamWriter sw = new StreamWriter(task9File))
            {
                sw.WriteLine("Привет, как дела?");
                sw.WriteLine("Это тестовый файл.");
                sw.WriteLine("Слова и буквы.");
            }
            Lab7.Task9_HashSet(task9File);

            Console.WriteLine("\nВсе задания выполнены. Нажмите любую клавишу для завершения.");
            Console.ReadKey();
            Console.WriteLine("\nВсе задания выполнены. Нажмите любую клавишу для завершения.");
            Console.ReadKey();
        }
    }
}
