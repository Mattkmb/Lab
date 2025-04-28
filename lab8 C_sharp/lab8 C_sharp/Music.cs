using System;

namespace MusicCatalog
{
    public class Music
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }

        public Music() { }

        public Music(int id, string title, string artist,
                     string genre, DateTime releaseDate,
                     TimeSpan duration, decimal price)
        {
            Id = id;
            Title = title;
            Artist = artist;
            Genre = genre;
            ReleaseDate = releaseDate;
            Duration = duration;
            Price = price;
        }

        public override string ToString()
        {
            return $"Id: {Id}, \"{Title}\", Исполнитель: {Artist}, " +
                   $"Жанр: {Genre}, Релиз: {ReleaseDate:yyyy-MM-dd}, " +
                   $"Длительность: {Duration:mm\\:ss}, Цена: {Price:C}";
        }
    }
}
