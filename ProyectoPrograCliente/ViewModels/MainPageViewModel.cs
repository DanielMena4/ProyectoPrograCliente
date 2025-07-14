using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoPrograCliente.Models;
using System.Collections.ObjectModel;

namespace ProyectoPrograCliente.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly MonsterService _monsterService;
    private readonly MonsterLocalService _monsterLocalService;
    private readonly INavigation _navigation;

    public MainViewModel(MonsterService monsterService, MonsterLocalService monsterLocalService, INavigation navigation)
    {
        _monsterService = monsterService;
        _monsterLocalService = monsterLocalService;
        _navigation = navigation;

        LoadMonstersCommand.Execute(null);
    }

    [ObservableProperty]
    ObservableCollection<Monster> monsters = new();

    [RelayCommand]
    async Task LoadMonstersAsync()
    {
        var localList = await _monsterLocalService.GetMonstersAsync();
        Monsters = new ObservableCollection<Monster>(localList);

        try
        {
            var apiList = await _monsterService.GetMonstersAsync();

            if (apiList != null && apiList.Any())
            {
                await _monsterLocalService.DeleteAllAsync();
                foreach (var monster in apiList)
                {
                    await _monsterLocalService.SaveMonsterAsync(monster);
                }
                Monsters = new ObservableCollection<Monster>(apiList);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener desde API: {ex.Message}");
        }
    }


    [RelayCommand]
    async Task AddMonsterAsync()
    {
        await _navigation.PushAsync(new MonsterFormPage(_monsterService, _monsterLocalService));
    }

    [RelayCommand]
    async Task SelectMonsterAsync(Monster selectedMonster)
    {
        if (selectedMonster is null) return;
        await _navigation.PushAsync(new MonsterFormPage(_monsterService, _monsterLocalService, selectedMonster));
    }

    [RelayCommand]
    async Task DeleteMonsterAsync(Monster monster)
    {
        if (monster is null) return;
        bool confirm = await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Eliminar a {monster.MonsterName}?", "Sí", "No");
        if (confirm)
        {
            try
            {
                await _monsterService.DeleteMonsterAsync(monster.Id);
            }
            catch
            {
            }
            await _monsterLocalService.DeleteMonsterAsync(monster);
            await LoadMonstersAsync();
        }
    }
}

