using System.Diagnostics.CodeAnalysis;

namespace MusicManager.ConApp
{
    internal class Program
    {
        static string CsvPath = "Data";
        static void Main(string[] args)
        {
            Console.WriteLine("Import von MusicDaten");

            using var albumRepo = new Repository.Logic.Repos.AlbumRepository(@"C:\Temp\album.json");
            using var trackRepo = new Repository.Logic.Repos.TrackRepository(@"C:\Temp\track.json");
            var albums = File.ReadAllLines(Path.Combine(CsvPath, "Album.csv"), System.Text.Encoding.UTF8).Skip(1);
            var artists = File.ReadAllLines(Path.Combine(CsvPath, "Artist.csv"), System.Text.Encoding.UTF8).Skip(1);
            var tracks = File.ReadAllLines(Path.Combine(CsvPath, "Track.csv"), System.Text.Encoding.UTF8).Skip(1);

            albumRepo.Clear();
            trackRepo.Clear();

            foreach (var lineAlbum in albums)
            {
                var albumData = lineAlbum.Split(";");
                var album = albumRepo.Create();
                var artistData = artists.Select(l => l.Split(";")).FirstOrDefault(d => d[0] == albumData[2]);

                album.Title = albumData[1];
                if (artistData != null)
                {
                    album.Interpreter = artistData[1];
                }
                albumRepo.Add(album);

                foreach (var trackData in tracks.Select(l => l.Split(";")).Where(d => d[2] == albumData[0]))
                {
                    var track = trackRepo.Create();

                    track.AlbumId = album.Id;
                    if (trackData != null)
                    {
                        track.Name = trackData[1];
                        track.Composer = trackData[5];
                    }
                    trackRepo.Add(track);
                }
            }
            albumRepo.SaveChanges();
            trackRepo.SaveChanges();
        }
    }
}