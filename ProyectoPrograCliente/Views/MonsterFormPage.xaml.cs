using ProyectoPrograCliente.ViewModels;
using ProyectoPrograCliente.Models; 


namespace ProyectoPrograCliente;

public partial class MonsterFormPage : ContentPage
{
    public MonsterFormPage(MonsterService monsterService, Monster? monster = null)
    {
        InitializeComponent();
        BindingContext = new MonsterFormViewModel(monsterService, Navigation, monster);
    }
}
