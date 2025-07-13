using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoPrograCliente.Models;


namespace ProyectoPrograCliente.ViewModels;

public partial class MonsterFormViewModel : ObservableObject
{
    private readonly MonsterService _monsterService;
    private readonly INavigation _navigation;

    public MonsterFormViewModel(MonsterService monsterService, INavigation navigation, Monster? monster = null)
    {
        _monsterService = monsterService;
        _navigation = navigation;
        Monster = monster ?? new Monster();
        isEdit = monster != null;
    }

    [ObservableProperty]
    Monster monster;

    private readonly bool isEdit;

    [RelayCommand]
    public async Task SaveAsync()
    {
        bool result = isEdit
            ? await _monsterService.UpdateMonsterAsync(Monster)
            : await _monsterService.AddMonsterAsync(Monster);

        if (result)
        {
            await Application.Current.MainPage.DisplayAlert("Éxito", isEdit ? "Monstruo actualizado" : "Monstruo agregado", "OK");
            await _navigation.PopAsync();
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un problema al guardar", "OK");
        }
    }
}
