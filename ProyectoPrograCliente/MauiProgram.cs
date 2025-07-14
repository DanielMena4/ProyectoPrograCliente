using System.IO;
using Microsoft.Extensions.Logging.Debug;
using ProyectoPrograCliente.ViewModels;

namespace ProyectoPrograCliente
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("PressStart2P-Regular.ttf", "PixelFont");
                });

            builder.Services.AddSingleton<MonsterService>(sp =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7117/api/monsterApi/")
                };
                return new MonsterService(httpClient);
            });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "monsters.db3");
            builder.Services.AddSingleton(new MonsterLocalService(dbPath));

            builder.Services.AddTransient<MainViewModel>(sp =>
            {
                var monsterService = sp.GetRequiredService<MonsterService>();
                var monsterLocalService = sp.GetRequiredService<MonsterLocalService>();
                return new MainViewModel(monsterService, monsterLocalService, null!);
            });

            builder.Services.AddSingleton<MainPage>();


            return builder.Build();
        }
    }
}
