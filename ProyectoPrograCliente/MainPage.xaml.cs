using ProyectoPrograCliente.Models;


namespace ProyectoPrograCliente;

public partial class MainPage : ContentPage
{
    private readonly MonsterService _monsterService;

    public MainPage(MonsterService monsterService)
    {
        InitializeComponent();
        _monsterService = monsterService;
        LoadMonsters();
    }

    private async void LoadMonsters()
    {
        try
        {
            var monsters = await _monsterService.GetMonstersAsync();
            MonsterList.ItemsSource = monsters;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la lista de monstruos.\n{ex.Message}", "OK");
        }
    }
}
