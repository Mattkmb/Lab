using System;
using System.Collections.Generic;

namespace MusicCatalog
{
  public static class SampleData
  {
    public static List<Music> GetInitialTracks() => new List<Music>
    {
 new Music(1, "Группа крови", "Кино", "Rock", new DateTime(1988, 6, 6), new TimeSpan(0,4,56), 49.99m),
 new Music(2, "Кукушка", "Кино", "Rock",new DateTime(1987, 5, 31), new TimeSpan(0,4,26), 45.00m),
 new Music(3, "Пачка сигарет","Кино","Rock",new DateTime(1989, 5, 17),new TimeSpan(0,4,10), 42.00m),
 new Music(4, "Земля в иллюминаторе", "Машина времени", "Classic Rock",new DateTime(1982,10,27), new TimeSpan(0,5,23), 39.99m),
 new Music(5, "Экспонат", "Ленинград", "Ska Punk", new DateTime(2012,2,14),  new TimeSpan(0,3,02), 34.99m),
 new Music(6, "На заре", "Альянс", "New Wave", new DateTime(1987,4,15),  new TimeSpan(0,3,57), 30.00m),
 new Music(7, "Комбат", "Любэ", "Pop-Rock", new DateTime(1995,2,14),  new TimeSpan(0,3,45), 29.99m),
 new Music(8, "Полковнику никто не пишет", "Зимовье зверей",   "Folk-Rock",  new DateTime(1994,7,1),  new TimeSpan(0,5,12), 39.50m),
 new Music(9, "Штиль", "Ария", "Heavy Metal", new DateTime(1987,3,10),  new TimeSpan(0,5,23), 50.00m),
 new Music(10, "Вечная любовь",  "Александр Маршал",  "Hard Rock",  new DateTime(1993,9,1),  new TimeSpan(0,4,15), 37.50m)
    };
  }
}
