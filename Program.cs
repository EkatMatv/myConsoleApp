using Microsoft.Extensions.DependencyInjection;

namespace myConsoleApp
{
    public interface ILogger
    {
        void Log(string message);
    }

    public interface IMusic
    {
        string GetGenre();
    }

    public interface IMusicPlayer
    {
        void PlayMusicWithMessage(string message);
        void DisplayCurrentGenre();
    }
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}");
        }
    }

    public class ClassicalMusic : IMusic
    {
        public string GetGenre()
        {
            return "Классическая";
        }
    }

    public class RockMusic : IMusic
    {
        public string GetGenre()
        {
            return "Рок";
        }
    }

    public class JazzMusic : IMusic
    {
        public string GetGenre()
        {
            return "Джаз";
        }
    }

    public class PopMusic : IMusic
    {
        public string GetGenre()
        {
            return "Поп";
        }
    }

    public class MusicPlayer : IMusicPlayer
    {
        private readonly ILogger _logger;
        private readonly IMusic _music;

        public MusicPlayer(ILogger logger, IMusic music)
        {
            _logger = logger;
            _music = music;
        }

        public void PlayMusicWithMessage(string message)
        {
            string genre = _music.GetGenre();
            _logger.Log($"Играет {genre} музыка. {message}");
        }

        public void DisplayCurrentGenre()
        {
            _logger.Log($"Музыкальный жанр: {_music.GetGenre()}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!\n");

            var services = new ServiceCollection();
            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddSingleton<IMusic, ClassicalMusic>();
            services.AddSingleton<IMusicPlayer, MusicPlayer>();
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger>();
            logger.Log("Программа запущена!");
            var music = serviceProvider.GetRequiredService<IMusic>();
            Console.WriteLine($"Музыкальный жанр: {music.GetGenre()} музыка");
            logger.Log($"Сейчас играет: {music.GetGenre()} музыка");

            var musicPlayer = serviceProvider.GetRequiredService<IMusicPlayer>();

            musicPlayer.DisplayCurrentGenre();            
            musicPlayer.PlayMusicWithMessage("Моя любимая мелодия!");
            logger.Log("Программа завершает работу!");
        }
    }
}
