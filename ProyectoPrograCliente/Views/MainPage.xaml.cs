using ProyectoPrograCliente.ViewModels;

namespace ProyectoPrograCliente;

public partial class MainPage : ContentPage
{
    public MainPage(MonsterService monsterService)
    {
        InitializeComponent();
        BindingContext = new MainViewModel(monsterService, Navigation);
    }
}
