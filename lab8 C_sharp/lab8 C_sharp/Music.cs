using System;

namespace MusicCatalog
{
    /// <summary>
    /// Представляет одну запись каталога музыки.
    /// </summary>
    public class Music
    {
        /// <summary>Уникальный идентификатор трека.</summary>
        private int Id { get; }

        /// <summary>Название трека.</summary>
        private string Title { get; }

        /// <summary>Исполнитель.</summary>
        private string Artist { get; }

        /// <summary>Жанр.</summary>
        private string Genre { get; }

        /// <summary>Дата релиза.</summary>
        private DateTime ReleaseDate { get; }

        /// <summary>Длительность трека.</summary>
        private TimeSpan Duration { get; }

        /// <summary>Цена.</summary>
        private decimal Price { get; }

        /// <summary>
        /// Создаёт новую запись трека.
        /// </summary>
        /// <param name="id">Идентификатор (должен быть > 0).</param>
        /// <param name="title">Название (не пустое).</param>
        /// <param name="artist">Исполнитель (не пустой).</param>
        /// <param name="genre">Жанр (не пустой).</param>
        /// <param name="releaseDate">Дата релиза.</param>
        /// <param name="duration">Длительность (не отрицательная).</param>
        /// <param name="price">Цена (>= 0).</param>
        /// <exception cref="ArgumentException">Если title или artist пусты.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Если id ≤ 0, price < 0 или duration &lt; 0.</exception>
        public Music(int id,
                     string title,
                     string artist,
                     string genre,
                     DateTime releaseDate,
                     TimeSpan duration,
                     decimal price)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id должен быть положительным");
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title не может быть пустым", nameof(title));
            if (string.IsNullOrWhiteSpace(artist))
                throw new ArgumentException("Artist не может быть пустым", nameof(artist));
            if (string.IsNullOrWhiteSpace(genre))
                throw new ArgumentException("Genre не может быть пустым", nameof(genre));
            if (duration < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(duration), "Duration не может быть отрицательной");
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price не может быть отрицательной");

            Id = id;
            Title = title;
            Artist = artist;
            Genre = genre;
            ReleaseDate = releaseDate;
            Duration = duration;
            Price = price;
        }

        /// <summary>
        /// Возвращает строковое представление трека.
        /// </summary>
        public override string ToString()
        {
            // Дата в формате dd-MM-yyyy, длительность mm:ss
            return $"Id: {Id}, \"{Title}\", Исполнитель: {Artist}, " +
                   $"Жанр: {Genre}, Релиз: {ReleaseDate:dd-MM-yyyy}, " +
                   $"Длительность: {Duration:mm\\:ss}, Цена: {Price:C}";
        }
    }
}
