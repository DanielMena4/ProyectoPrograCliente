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

    private async void OnAddMonsterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MonsterFormPage(_monsterService));
    }

    private async void OnMonsterSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Monster selectedMonster)
        {
            await Navigation.PushAsync(new MonsterFormPage(_monsterService, selectedMonster));
        }

        MonsterList.SelectedItem = null;
    }

    private async void OnDeleteMonster(object sender, EventArgs e)
    {
        if ((sender as Button)?.CommandParameter is Monster monster)
        {
            bool confirm = await DisplayAlert("Eliminar", $"¿Eliminar a {monster.MonsterName}?", "Sí", "No");
            if (confirm)
            {
                await _monsterService.DeleteMonsterAsync(monster.Id);
                LoadMonsters();
            }
        }
    }



    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadMonsters();
    }
}
