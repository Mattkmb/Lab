using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicCatalog
{
    /// <summary>
    /// Основной класс консольного приложения.
    /// </summary>
    class Program
    {
        private const string FilePath = "musicdb.bin";

        static void Main()
        {
            // При первом запуске создаём файл с примерными данными
            /*if (!File.Exists(FilePath))
            {
                var initial = SampleData.GetInitialTracks();
                DBHelper.SaveToFile(FilePath, initial);
                Console.WriteLine($"Создана стартовая база: {FilePath}");
            }*/

            var musicList = DBHelper.LoadFromFile(FilePath);

            while (true)
            {
                Console.WriteLine("\n===== Каталог музыки =====");
                Console.WriteLine("1. Загрузить базу");
                Console.WriteLine("2. Просмотреть базу");
                Console.WriteLine("3. Добавить запись");
                Console.WriteLine("4. Удалить запись");
                Console.WriteLine("5. Запросы");
                Console.WriteLine("6. Сохранить базу");
                Console.WriteLine("0. Выход");
                string choice = InputValidator.ReadString("Выберите опцию: ");

                switch (choice)
                {
                    case "1":
                        musicList = DBHelper.LoadFromFile(FilePath);
                        break;

                    case "2":
                        DisplayAll(musicList);
                        break;

                    case "3":
                        AddRecord(musicList);
                        break;

                    case "4":
                        DeleteRecord(musicList);
                        break;

                    case "5":
                        ShowQueries(musicList);
                        break;

                    case "6":
                        DBHelper.SaveToFile(FilePath, musicList);
                        break;

                    case "0":
                        DBHelper.SaveToFile(FilePath, musicList);
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Повторите.");
                        break;
                }
            }
        }

        private static void DisplayAll(List<Music> list)
        {
            if (list.Count == 0)
                Console.WriteLine("База пуста.");
            else
                list.ForEach(m => Console.WriteLine(m));
        }

        private static void AddRecord(List<Music> list)
        {
            int newId = list.Count > 0 ? list.Max(m => m.Id) + 1 : 1;
            string title = InputValidator.ReadString("Название: ");
            string artist = InputValidator.ReadString("Исполнитель: ");
            string genre = InputValidator.ReadString("Жанр: ");
            DateTime rd = InputValidator.ReadDate("Дата релиза (dd-MM-yyyy): ");
            TimeSpan dur = InputValidator.ReadTimeSpan("Длительность (mm:ss): ");
            decimal price = InputValidator.ReadDecimal("Цена: ");

            var track = new Music(newId, title, artist, genre, rd, dur, price);
            DBHelper.AddMusic(list, track);
            Console.WriteLine("Запись добавлена.");
        }

        private static void DeleteRecord(List<Music> list)
        {
            int id = InputValidator.ReadInt("Введите Id для удаления: ");
            bool removed = DBHelper.DeleteMusic(list, id);
            Console.WriteLine(removed ? "Запись удалена." : "Запись не найдена.");
        }

        private static void ShowQueries(List<Music> list)
        {
            while (true)
            {
                Console.WriteLine("\n--- Запросы ---");
                Console.WriteLine("1. Треки по исполнителю");
                Console.WriteLine("2. Треки по жанру");
                Console.WriteLine("3. Количество треков исполнителя");
                Console.WriteLine("4. Средняя длительность трека");
                Console.WriteLine("0. Назад");
                string q = InputValidator.ReadString("Ваш выбор: ");

                switch (q)
                {
                    case "1":
                        var art = InputValidator.ReadString("Исполнитель: ");
                        DisplayAll(DBHelper.GetByArtist(list, art));
                        break;

                    case "2":
                        var gen = InputValidator.ReadString("Жанр: ");
                        DisplayAll(DBHelper.GetByGenre(list, gen));
                        break;

                    case "3":
                        art = InputValidator.ReadString("Исполнитель: ");
                        Console.WriteLine($"Найдено треков: {DBHelper.CountByArtist(list, art)}");
                        break;

                    case "4":
                        var avg = DBHelper.AverageDuration(list);
                        Console.WriteLine($"Средняя длительность: {avg:mm\\:ss}");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Неверная опция.");
                        break;
                }
            }
        }
    }
}
