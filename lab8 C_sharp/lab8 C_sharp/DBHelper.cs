using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicCatalog
{
    /// <summary>
    /// Вспомогательные методы для работы с «базой» в бинарном файле.
    /// </summary>
    public static class DBHelper
    {
        /// <summary>Загружает список треков из файла.</summary>
        public static List<Music> LoadFromFile(string filePath)
        {
            var list = new List<Music>();
            try
            {
                using var fs = new FileStream(filePath, FileMode.Open);
                using var reader = new BinaryReader(fs);

                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    int id = reader.ReadInt32();
                    string title = reader.ReadString();
                    string artist = reader.ReadString();
                    string genre = reader.ReadString();
                    DateTime rd = new DateTime(reader.ReadInt64());
                    TimeSpan dur = new TimeSpan(reader.ReadInt64());
                    decimal price = reader.ReadDecimal();

                    list.Add(new Music(id, title, artist, genre, rd, dur, price));
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не найден. Будет создана новая база.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
            return list;
        }

        /// <summary>Сохраняет список треков в файл.</summary>
        public static void SaveToFile(string filePath, List<Music> list)
        {
            try
            {
                using var fs = new FileStream(filePath, FileMode.Create);
                using var writer = new BinaryWriter(fs);

                writer.Write(list.Count);
                foreach (var m in list)
                {
                    writer.Write(m.Id);
                    writer.Write(m.Title);
                    writer.Write(m.Artist);
                    writer.Write(m.Genre);
                    writer.Write(m.ReleaseDate.Ticks);
                    writer.Write(m.Duration.Ticks);
                    writer.Write(m.Price);
                }
                Console.WriteLine("База успешно сохранена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        /// <summary>Добавляет трек в список.</summary>
        public static void AddMusic(List<Music> list, Music music)
            => list.Add(music);

        /// <summary>Удаляет трек по указанному Id.</summary>
        public static bool DeleteMusic(List<Music> list, int id)
        {
            var query = from m in list
                        where m.Id == id
                        select m;

            var track = query.FirstOrDefault();
            if (track != null)
            {
                list.Remove(track);
                return true;
            }
            return false;
        }

        /// <summary>Возвращает все треки данного исполнителя.</summary>
        public static List<Music> GetByArtist(List<Music> list, string artist)
        {
            var query = from m in list
                        where m.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase)
                        select m;
            return query.ToList();
        }

        /// <summary>Возвращает все треки указанного жанра.</summary>
        public static List<Music> GetByGenre(List<Music> list, string genre)
        {
            var query = from m in list
                        where m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)
                        select m;
            return query.ToList();
        }

        /// <summary>Считает количество треков данного исполнителя.</summary>
        public static int CountByArtist(List<Music> list, string artist)
        {
            var query = from m in list
                        where m.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase)
                        select m;
            return query.Count();
        }

        /// <summary>Вычисляет среднюю длительность трека.</summary>
        public static TimeSpan AverageDuration(List<Music> list)
        {
            if (!list.Any())
                return TimeSpan.Zero;

            var query = from m in list
                        select m.Duration.TotalSeconds;

            double avgSec = query.Average();
            return TimeSpan.FromSeconds(avgSec);
        }
    }
}
