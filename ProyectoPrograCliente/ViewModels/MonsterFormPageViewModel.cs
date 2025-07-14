using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoPrograCliente.Models;


namespace ProyectoPrograCliente.ViewModels;

public partial class MonsterFormViewModel : ObservableObject
{
    private readonly MonsterService _monsterService;
    private readonly INavigation _navigation;
    private readonly MonsterLocalService _monsterLocalService;

    public MonsterFormViewModel(MonsterService monsterService, MonsterLocalService monsterLocalService, INavigation navigation, Monster? monster = null)
    {
        _monsterService = monsterService;
        _monsterLocalService = monsterLocalService;
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
        bool result = false;

        try
        {
            if (isEdit)
            {
                result = await _monsterService.UpdateMonsterAsync(Monster);
            }
            else
            {
                result = await _monsterService.AddMonsterAsync(Monster);
            }
        }
        catch
        {
        }

        await _monsterLocalService.SaveMonsterAsync(Monster);

        if (result)
        {
            await Application.Current.MainPage.DisplayAlert("Éxito", isEdit ? "Monstruo actualizado" : "Monstruo agregado", "OK");
            await _navigation.PopAsync();
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Aviso", "Guardado localmente. Se sincronizará cuando haya conexión.", "OK");
            await _navigation.PopAsync();
        }
    }

}
