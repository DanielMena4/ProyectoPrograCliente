using ProyectoPrograCliente.Models;

namespace ProyectoPrograCliente;

public partial class MonsterFormPage : ContentPage
{
    private readonly MonsterService _monsterService;
    public Monster Monster { get; set; }
    private bool isEdit;

    public MonsterFormPage(MonsterService monsterService, Monster? monster = null)
    {
        InitializeComponent();
        _monsterService = monsterService;

        Monster = monster ?? new Monster();
        isEdit = monster != null;

        BindingContext = this;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        bool result;

        if (isEdit)
            result = await _monsterService.UpdateMonsterAsync(Monster);
        else
            result = await _monsterService.AddMonsterAsync(Monster);

        if (result)
        {
            await DisplayAlert("Éxito", isEdit ? "Monstruo actualizado" : "Monstruo agregado", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Ocurrió un problema al guardar", "OK");
        }
    }
}
