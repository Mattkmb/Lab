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
            string task1File = InputHandler.ReadString("Введите имя файла для задачи 1: ");
            Lab7.FillTextFileTask1(task1File, 10);
            Lab7.Task1(task1File);

            // ЗАДАНИЕ 2. Текстовый файл: вычислить произведение элементов.
            Console.WriteLine("\n=== ЗАДАНИЕ 2 ===");
            string task2File = InputHandler.ReadString("Введите имя файла для задачи 2: ");
            Lab7.FillTextFileTask2(task2File, 5, 4);//5 строк по 4 числа
            Lab7.Task2(task2File);

            // ЗАДАНИЕ 3. Текстовый файл: переписать в другой файл строки, имеющие заданную длину m.
            Console.WriteLine("\n=== ЗАДАНИЕ 3 ===");
            string task3Input = InputHandler.ReadString("Введите имя входного файла для задачи 3: ");
            string task3Output = InputHandler.ReadString("Введите имя выходного файла для задачи 3: ");
            int m = InputHandler.ReadInt("Введите требуемую длину строк: ");
            int lineCount = InputHandler.ReadInt("Введите количество строк для входного файла (задача 3): ");
            using (StreamWriter sw = new StreamWriter(task3Input))
            {
                for (int i = 0; i < lineCount; i++)
                {
                    sw.WriteLine(InputHandler.ReadString($"Строка {i + 1}: "));
                }
            }
            Lab7.Task3(task3Input, task3Output, m);
            Console.WriteLine("Содержимое выходного файла задачи 3:");
            foreach (var line in File.ReadAllLines(task3Output))
                Console.WriteLine(line);

            // ЗАДАНИЕ 4. Бинарный файл: вычислить произведение нечетных отрицательных чисел.
            Console.WriteLine("\n=== ЗАДАНИЕ 4 ===");
            string task4File = InputHandler.ReadString("Введите имя бинарного файла для задачи 4: ");
            Lab7.FillBinaryFileTask4(task4File, 20);
            Lab7.Task4(task4File);

            // ЗАДАНИЕ 5. XML-сериализация (багаж пассажиров)
            Console.WriteLine("\n=== ЗАДАНИЕ 5 ===");
            string task5File = InputHandler.ReadString("Введите имя XML файла для задачи 5: ");
            int passengerCount = InputHandler.ReadInt("Введите количество пассажиров: ");
            int minItems = InputHandler.ReadInt("Введите минимальное число единиц багажа: ");
            int maxItems = InputHandler.ReadInt("Введите максимальное число единиц багажа: ");
            Lab7.FillXMLFileTask5(task5File, passengerCount, minItems, maxItems);
            Lab7.Task5(task5File);

            // ЗАДАНИЕ 6. List: заменить первое вхождение подсписка L1 в L на L2.
            Console.WriteLine("\n=== ЗАДАНИЕ 6 ===");
            int listSize = InputHandler.ReadInt("Введите количество элементов основного списка L: ");
            List<int> L = new List<int>();
            Console.WriteLine("Введите элементы списка L:");
            for (int i = 0; i < listSize; i++)
                L.Add(InputHandler.ReadInt($"Элемент {i + 1}: "));
            int list1Size = InputHandler.ReadInt("Введите количество элементов подсписка L1: ");
            List<int> L1 = new List<int>();
            Console.WriteLine("Введите элементы подсписка L1:");
            for (int i = 0; i < list1Size; i++)
                L1.Add(InputHandler.ReadInt($"Элемент {i + 1} L1: "));
            int list2Size = InputHandler.ReadInt("Введите количество элементов списка L2: ");
            List<int> L2 = new List<int>();
            Console.WriteLine("Введите элементы списка L2:");
            for (int i = 0; i < list2Size; i++)
                L2.Add(InputHandler.ReadInt($"Элемент {i + 1} L2: "));
            Lab7.ReplaceFirstOccurrence<int>(L, L1, L2);

            // ЗАДАНИЕ 7. LinkedList: сортировать элементы списка по возрастанию.
            Console.WriteLine("\n=== ЗАДАНИЕ 7 ===");
            int linkedSize = InputHandler.ReadInt("Введите количество элементов LinkedList: ");
            LinkedList<int> linkedList = new LinkedList<int>();
            Console.WriteLine("Введите элементы LinkedList:");
            for (int i = 0; i < linkedSize; i++)
                linkedList.AddLast(InputHandler.ReadInt($"Элемент {i + 1}: "));
            Lab7.Task7_LinkedList(linkedList);

            // ЗАДАНИЕ 8. HashSet (Компьютерные игры).
            Console.WriteLine("\n=== ЗАДАНИЕ 8 ===");
            int masterCount = InputHandler.ReadInt("Сколько игр в мастер-списке? ");
            List<string> masterGames = new List<string>();
            Console.WriteLine("Введите названия игр для мастер-списка:");
            for (int i = 0; i < masterCount; i++)
                masterGames.Add(InputHandler.ReadString($"Игра {i + 1}: "));
            int studentCount = InputHandler.ReadInt("Сколько студентов в группе? ");
            List<HashSet<string>> studentGames = new List<HashSet<string>>();
            for (int i = 0; i < studentCount; i++)
            {
                int gamesCount = InputHandler.ReadInt($"Сколько игр у студента {i + 1}? ");
                HashSet<string> gamesSet = new HashSet<string>();
                Console.WriteLine($"Введите игры для студента {i + 1}:");
                for (int j = 0; j < gamesCount; j++)
                    gamesSet.Add(InputHandler.ReadString($"Игра {j + 1}: "));
                studentGames.Add(gamesSet);
            }
            Lab7.Task8_HashSet(masterGames, studentGames);

            // ЗАДАНИЕ 9. HashSet (Глухие согласные из текстового файла).
            Console.WriteLine("\n=== ЗАДАНИЕ 9 ===");
            string task9File = InputHandler.ReadString("Введите имя файла с текстом для задачи 9: ");
            int textLines = InputHandler.ReadInt("Введите количество строк текста: ");
            using (StreamWriter sw = new StreamWriter(task9File))
            {
                for (int i = 0; i < textLines; i++)
                    sw.WriteLine(InputHandler.ReadString($"Строка {i + 1}: "));
            }
            Lab7.Task9_HashSet(task9File);

            Console.WriteLine("\nВсе задания выполнены. Нажмите любую клавишу для завершения.");
            Console.ReadKey();
        }
    }
}
