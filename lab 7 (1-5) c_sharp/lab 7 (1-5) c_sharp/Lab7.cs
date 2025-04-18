using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lab7Project
{
    public class Lab7
    {
        // ============================
        // ЗАДАНИЕ 1. Текстовый файл
        // (В файле найти сумму квадратов элементов)
        // ============================
        public static void FillTextFileTask1(string filename, int count)
        {
            Random rand = new Random();
            using (StreamWriter sw = new StreamWriter(filename))
            {
                for (int i = 0; i < count; i++)
                {
                    int num = rand.Next(-100, 101);
                    sw.WriteLine(num);
                }
            }
            Console.WriteLine($"Task1: Файл \"{filename}\" заполнен {count} случайными числами.");
        }

        public static void Task1(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            long sumOfSquares = 0;
            foreach (string line in lines)
            {
                if (int.TryParse(line, out int num))
                {
                    sumOfSquares += (long)num * num;
                }
            }
            Console.WriteLine("Task1  : Сумма квадратов элементов = " + sumOfSquares);
        }

        // ============================
        // ЗАДАНИЕ 2. Текстовый файл
        // (В файле вычислить произведение элементов)
        // ============================
        public static void FillTextFileTask2(string filename, int rows, int numbersPerRow)
        {
            
            Random rand = new Random();
            using (StreamWriter sw = new StreamWriter(filename))
            {
                for (int i = 0; i < rows; i++)
                {
                    string line = "";
                    for (int j = 0; j < numbersPerRow; j++)
                    {
                        int num = rand.Next(1, 11);
                        line += num.ToString();
                        if (j < numbersPerRow - 1)
                        {
                            line += " ";
                        }
                    }
                    sw.WriteLine(line);
                }
            }
            Console.WriteLine($"Task2: Файл \"{filename}\" заполнен {rows} строками по {numbersPerRow} чисел.");
        }

        public static void Task2(string filename)
        {
            long product = 1;
            bool foundAny = false;
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string token in tokens)
                    {
                        if (int.TryParse(token, out int num))
                        {
                            product *= num;
                            foundAny = true;
                        }
                    }
                }
            }
            if (!foundAny)
                Console.WriteLine("Task2: В файле не найдено чисел.");
            else
                Console.WriteLine("Task2  : Произведение элементов = " + product);
        }

        // ============================
        // ЗАДАНИЕ 3. Текстовый файл
        // (Переписать в другой файл строки, имеющие заданную длину m)
        // ============================
        
        public static void FillTextFileTask3(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine("Привет");
                sw.WriteLine("Мир");
                sw.WriteLine("Код");
                sw.WriteLine("Пирожочек");
                sw.WriteLine("Тест");
            }
            Console.WriteLine($"Task3: Входной файл \"{filename}\" создан.");
        }

        public static void Task3(string inputFilename, string outputFilename, int m)
        {
            string[] lines = File.ReadAllLines(inputFilename);
            using (StreamWriter sw = new StreamWriter(outputFilename))
            {
                foreach (string line in lines)
                {
                    if (line.Length == m)
                        sw.WriteLine(line);
                }
            }
            Console.WriteLine($"Task3  : Строки длиной {m} символов записаны в файл \"{outputFilename}\".");
        }

        // ============================
        // ЗАДАНИЕ 4. Бинарный файл
        // (В бинарном файле вычислить произведение нечетных отрицательных чисел)
        // ============================
        public static void FillBinaryFileTask4(string filename, int count)
        {
            Random rand = new Random();
            using (BinaryWriter bw = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    int num = rand.Next(-100, 101);
                    bw.Write(num);
                }
            }
            Console.WriteLine($"Task4: Бинарный файл \"{filename}\" заполнен {count} случайными числами.");
        }

        public static void Task4(string filename)
        {
            List<int> numbers = new List<int>();
            using (BinaryReader br = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                    numbers.Add(br.ReadInt32());
            }
            long product = 1;
            bool found = false;
            foreach (int num in numbers)
            {
                if (num < 0 && num % 2 != 0) 
                {
                    product *= num;
                    found = true;
                }
            }
            if (found)
                Console.WriteLine("Task4  : Произведение нечетных отрицательных чисел = " + product);
            else
                Console.WriteLine("Task4  : Отрицательных нечетных чисел не найдено.");
        }

        // ============================
        // ЗАДАНИЕ 5. Бинарные файлы и структуры (XML-сериализация)
        // (Информация о багаже пассажира: найти число пассажиров с более чем 2 единицами багажа и число пассажиров, у которых количество единиц багажа выше среднего)
        // ============================
        [Serializable]
        public class BaggageItem
        {
            public string ItemName { get; set; }
            public double Mass { get; set; }

            
            public BaggageItem() { }

            public BaggageItem(string name, double mass)
            {
                ItemName = name;
                Mass = mass;
            }
        }

        [Serializable]
        public class PassengerBaggage
        {
            public List<BaggageItem> Items { get; set; }
            public PassengerBaggage()
            {
                Items = new List<BaggageItem>();
            }
        }

        [Serializable]
        [XmlRoot("PassengersBaggage")]
        public class PassengersBaggageList
        {
            [XmlElement("Passenger")]
            public List<PassengerBaggage> Passengers { get; set; }
            public PassengersBaggageList()
            {
                Passengers = new List<PassengerBaggage>();
            }
        }

        public static void FillXMLFileTask5(string filename, int passengerCount, int minItems, int maxItems)
        {
            Random rand = new Random();
            string[] possibleItems = { "Чемодан", "Сумка", "Коробка", "Рюкзак", "Портфель" };
            PassengersBaggageList pbList = new PassengersBaggageList();

            for (int i = 0; i < passengerCount; i++)
            {
                PassengerBaggage passenger = new PassengerBaggage();
                int itemsCount = rand.Next(minItems, maxItems + 1);
                for (int j = 0; j < itemsCount; j++)
                {
                    string name = possibleItems[rand.Next(possibleItems.Length)];
                    double mass = Math.Round(rand.NextDouble() * 25 + 5, 1); // масса от 5 до 30 кг
                    passenger.Items.Add(new BaggageItem(name, mass));
                }
                pbList.Passengers.Add(passenger);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(PassengersBaggageList));
            using (TextWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, pbList);
            }
            Console.WriteLine($"Task5: XML-файл \"{filename}\" создан с данными о {passengerCount} пассажирах.");
        }

        public static void Task5(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PassengersBaggageList));
            PassengersBaggageList pbList;
            using (TextReader reader = new StreamReader(filename))
            {
                pbList = (PassengersBaggageList)serializer.Deserialize(reader);
            }
            int countMoreThanTwo = 0;
            int totalItems = 0;
            int totalPassengers = pbList.Passengers.Count;
            foreach (PassengerBaggage passenger in pbList.Passengers)
            {
                int items = passenger.Items.Count;
                totalItems += items;
                if (items > 2)
                    countMoreThanTwo++;
            }
            double averageItems = totalPassengers > 0 ? (double)totalItems / totalPassengers : 0;

            int countAboveAverage = 0;
            foreach (PassengerBaggage passenger in pbList.Passengers)
            {
                if (passenger.Items.Count > averageItems)
                    countAboveAverage++;
            }
            Console.WriteLine("Task5  :");
            Console.WriteLine($"  Число пассажиров с более чем 2 единицами багажа = {countMoreThanTwo}");
            Console.WriteLine($"  Число пассажиров, у которых количество багажа выше среднего ({averageItems:F2}) = {countAboveAverage}");
        }

        // ============================
        // ЗАДАНИЕ 6. List
        // (В списке L заменить первое вхождение списка L1 (если такое есть) на список L2)
        // ============================
        public static void Task6_List(List<int> L, List<int> L1, List<int> L2)
        {
            if (L1 == null || L1.Count == 0)
            {
                Console.WriteLine("Task6: L1 пуст, нечего заменять.");
                return;
            }

            int pos = -1;
            for (int i = 0; i <= L.Count - L1.Count; i++)
            {
                bool match = true;
                for (int j = 0; j < L1.Count; j++)
                {
                    if (L[i + j] != L1[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    pos = i;
                    break;
                }
            }
            if (pos == -1)
            {
                Console.WriteLine("Task6: Подсписок L1 не найден в L.");
                return;
            }
            L.RemoveRange(pos, L1.Count);
            L.InsertRange(pos, L2);
            Console.Write("Task6  : Результирующий список: ");
            foreach (var item in L)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        // ============================
        // ЗАДАНИЕ 7. LinkedList
        // (Сортировать элементы списка по возрастанию)
        // ============================
        public static void Task7_LinkedList(LinkedList<int> list)
        {
            if (list == null || list.Count == 0)
            {
                Console.WriteLine("Task7: LinkedList пуст.");
                return;
            }
            List<int> temp = new List<int>(list);
            temp.Sort();
            Console.Write("Task7  : Отсортированный LinkedList: ");
            foreach (var item in temp)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        // ============================
        // ЗАДАНИЕ 8. HashSet
        // (Имеется перечень компьютерных игр. Для каждого студента задано, в какие игры он играет.
        //  Определить:
        //    – в какие игры из перечня играют все студенты,
        //    – в какие игры играют некоторые студенты,
        //    – в какие игры не играет ни один студент)
        // ============================
        public static void Task8_HashSet(List<string> masterGames, List<HashSet<string>> studentGames)
        {
            
            HashSet<string> union = new HashSet<string>();
        
            HashSet<string> intersection = (studentGames.Count > 0) ? new HashSet<string>(studentGames[0]) : new HashSet<string>();

            foreach (var games in studentGames)
            {
                union.UnionWith(games);
                intersection.IntersectWith(games);
            }
            // Вычисляем игры, в которые не играет ни один студент
            HashSet<string> notPlayed = new HashSet<string>(masterGames);
            notPlayed.ExceptWith(union);

            // Сортировка результатов
            List<string> allStudents = new List<string>(intersection);
            allStudents.Sort(StringComparer.CurrentCulture);
            List<string> someStudents = new List<string>(union);
            someStudents.Sort(StringComparer.CurrentCulture);
            List<string> nonePlayed = new List<string>(notPlayed);
            nonePlayed.Sort(StringComparer.CurrentCulture);

            Console.WriteLine("Task8  :");
            Console.WriteLine("  Игры, в которые играют все студенты: " + string.Join(", ", allStudents));
            Console.WriteLine("  Игры, в которые играют некоторые студенты: " + string.Join(", ", someStudents));
            Console.WriteLine("  Игры, в которые не играет ни один студент: " + string.Join(", ", nonePlayed));
        }

        // ============================
        // ЗАДАНИЕ 9. HashSet
        // (Файл содержит текст на русском языке. Напечатать в алфавитном порядке все глухие согласные буквы,
        // которые не входят хотя бы в одно слово)
        // ============================
        public static void Task9_HashSet(string filename)
        {
            // Определим множество глухих согласных (в нижнем регистре).
            // Используем следующие буквы: п, ф, к, т, с, ш, х, ц, ч.
            HashSet<char> voiceless = new HashSet<char> { 'п', 'ф', 'к', 'т', 'с', 'ш', 'х', 'ц', 'ч' };

            string text = File.ReadAllText(filename).ToLower();
            // Разбиваем текст на слова (используем стандартные разделители)
            char[] delimiters = { ' ', '\n', '\r', ',', '.', ';', ':', '-', '!', '?', '(', ')', '"' };
            string[] words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            List<char> result = new List<char>();
            // Для каждой глухой согласной проверяем: если хотя бы в одном слове её нет, то включаем в результат.
            foreach (char letter in voiceless)
            {
                bool inEvery = true;
                foreach (string word in words)
                {
                    if (!word.Contains(letter))
                    {
                        inEvery = false;
                        break;
                    }
                }
                if (!inEvery)
                    result.Add(letter);
            }
            result.Sort(); // Стандартная сортировка по коду символа (для русского это сработает, если используется Unicode).

            Console.Write("Task9  : Глухие согласные, отсутствующие хотя бы в одном слове: ");
            foreach (char letter in result)
                Console.Write(letter + " ");
            Console.WriteLine();
        }
    }
}
