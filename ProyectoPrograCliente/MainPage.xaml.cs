using ProyectoPrograCliente.Services;

namespace ProyectoPrograCliente
{
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
                monsterListView.ItemsSource = monsters;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load monsters: {ex.Message}", "OK");
            }
        }
    }

}
