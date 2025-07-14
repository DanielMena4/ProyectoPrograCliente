using ProyectoPrograCliente.ViewModels;

namespace ProyectoPrograCliente;

public partial class MainPage : ContentPage
{
    public MainPage(MonsterService monsterService, MonsterLocalService monsterLocalService)
    {
        InitializeComponent();
        BindingContext = new MainViewModel(monsterService, monsterLocalService, Navigation);
    }
}
