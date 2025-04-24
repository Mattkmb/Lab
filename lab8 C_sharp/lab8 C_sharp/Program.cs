using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MusicCatalog
{
    class Program
    {
        const string FilePath = "musicdb.bin";

        static void Main()
        {

          /*  // если bin‑файла ещё нет — создаём из SampleData
            if (!File.Exists(FilePath))
            {
                var init = SampleData.GetInitialTracks();
                DBHelper.SaveToFile(FilePath, init);
                Console.WriteLine($"Создана стартовая база: {FilePath}");
            }
*/
            var musicList = DBHelper.LoadFromFile(FilePath);

            while (true)
            {
                Console.WriteLine("\n===== Каталог музыки =====");
                Console.WriteLine("1. Загрузить базу из файла");
                Console.WriteLine("2. Просмотреть базу");
                Console.WriteLine("3. Добавить запись");
                Console.WriteLine("4. Удалить запись по Id");
                Console.WriteLine("5. Запросы");
                Console.WriteLine("6. Сохранить базу");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите опцию: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        musicList = DBHelper.LoadFromFile(FilePath);
                        break;

                    case "2":
                        if (musicList.Any())
                            musicList.ForEach(Console.WriteLine);
                        else
                            Console.WriteLine("База пуста.");
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
                        Console.WriteLine("Сохраняем и выходим...");
                        DBHelper.SaveToFile(FilePath, musicList);
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Повторите.");
                        break;
                }
            }
        }

        static void AddRecord(List<Music> list)
        {
            try
            {
                int newId = list.Any() ? list.Max(m => m.Id) + 1 : 1;
                Console.Write("Название: ");
                string title = Console.ReadLine();

                Console.Write("Исполнитель: ");
                string artist = Console.ReadLine();

                Console.Write("Жанр: ");
                string genre = Console.ReadLine();

                Console.Write("Дата релиза (День-Mесяц-Год): ");
                if (!DateTime.TryParseExact(
                        Console.ReadLine(),
                        "dd-MM-yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var rd))
                {
                    Console.WriteLine("Неверный формат даты. Ожидается День-Месяц-Год. Отмена.");
                    return;
                }


                Console.Write("Длительность (Минуты:Секунды): ");
                if (!TimeSpan.TryParseExact(Console.ReadLine(),
                        "m\\:ss", CultureInfo.InvariantCulture, out var duration))
                {
                    Console.WriteLine("Неверный формат длительности. Отмена.");
                    return;
                }

                Console.Write("Цена: ");
                if (!decimal.TryParse(Console.ReadLine(), out var price))
                {
                    Console.WriteLine("Неверный формат цены. Отмена.");
                    return;
                }

                var music = new Music(newId, title, artist, genre, rd, duration, price);
                DBHelper.AddMusic(list, music);
                Console.WriteLine("Запись добавлена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
            }
        }

        static void DeleteRecord(List<Music> list)
        {
            Console.Write("Введите Id для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный Id.");
                return;
            }
            if (DBHelper.DeleteMusic(list, id))
                Console.WriteLine("Запись удалена.");
            else
                Console.WriteLine("Запись с таким Id не найдена.");
        }

        static void ShowQueries(List<Music> list)
        {
            while (true)
            {
                Console.WriteLine("\n--- Запросы ---");
                Console.WriteLine("1. Треки по исполнителю");
                Console.WriteLine("2. Треки по жанру");
                Console.WriteLine("3. Количество треков исполнителя");
                Console.WriteLine("4. Средняя длительность трека");
                Console.WriteLine("0. Назад");
                Console.Write("Ваш выбор: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Исполнитель: ");
                        var art = Console.ReadLine();
                        var byArtist = DBHelper.GetByArtist(list, art);
                        if (byArtist.Any()) byArtist.ForEach(Console.WriteLine);
                        else Console.WriteLine("Нет записей.");
                        break;

                    case "2":
                        Console.Write("Жанр: ");
                        var gen = Console.ReadLine();
                        var byGenre = DBHelper.GetByGenre(list, gen);
                        if (byGenre.Any()) byGenre.ForEach(Console.WriteLine);
                        else Console.WriteLine("Нет записей.");
                        break;

                    case "3":
                        Console.Write("Исполнитель: ");
                        art = Console.ReadLine();
                        int cnt = DBHelper.CountByArtist(list, art);
                        Console.WriteLine($"Найдено треков: {cnt}");
                        break;

                    case "4":
                        var avg = DBHelper.AverageDuration(list);
                        Console.WriteLine($"Средняя длительность: {avg:mm\\:ss}");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Неверно. Пробуем снова.");
                        break;
                }
            }
        }
    }
}
