using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoPrograCliente.Models;
using System.Collections.ObjectModel;

namespace ProyectoPrograCliente.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly MonsterService _monsterService;
    private readonly INavigation _navigation;

    public MainViewModel(MonsterService monsterService, INavigation navigation)
    {
        _monsterService = monsterService;
        _navigation = navigation;
        LoadMonstersCommand.Execute(null);
    }

    [ObservableProperty]
    ObservableCollection<Monster> monsters = new();

    [RelayCommand]
    async Task LoadMonstersAsync()
    {
        var list = await _monsterService.GetMonstersAsync();
        Monsters = new ObservableCollection<Monster>(list);
    }

    [RelayCommand]
    async Task AddMonsterAsync()
    {
        await _navigation.PushAsync(new MonsterFormPage(_monsterService));
    }

    [RelayCommand]
    async Task SelectMonsterAsync(Monster selectedMonster)
    {
        if (selectedMonster is null) return;
        await _navigation.PushAsync(new MonsterFormPage(_monsterService, selectedMonster));
    }

    [RelayCommand]
    async Task DeleteMonsterAsync(Monster monster)
    {
        if (monster is null) return;
        bool confirm = await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Eliminar a {monster.MonsterName}?", "Sí", "No");
        if (confirm)
        {
            await _monsterService.DeleteMonsterAsync(monster.Id);
            await LoadMonstersAsync();
        }
    }
}
