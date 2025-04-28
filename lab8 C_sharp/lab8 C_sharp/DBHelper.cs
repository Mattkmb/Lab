using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicCatalog
{
    public static class DBHelper
    {
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

        public static void AddMusic(List<Music> list, Music music)
            => list.Add(music);

        public static bool DeleteMusic(List<Music> list, int id)
        {
            var m = list.FirstOrDefault(x => x.Id == id);
            if (m != null) { list.Remove(m); return true; }
            return false;
        }

        public static List<Music> GetByArtist(List<Music> list, string artist) =>
            list.Where(m => m.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                .ToList();

        public static List<Music> GetByGenre(List<Music> list, string genre) =>
            list.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                .ToList();

        public static int CountByArtist(List<Music> list, string artist) =>
            list.Count(m => m.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase));

        public static TimeSpan AverageDuration(List<Music> list)
        {
            if (!list.Any()) return TimeSpan.Zero;
            double avgSec = list.Average(m => m.Duration.TotalSeconds);
            return TimeSpan.FromSeconds(avgSec);
        }
    }
}
