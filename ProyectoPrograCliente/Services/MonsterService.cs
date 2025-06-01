// Services/MonsterService.cs
using System.Net.Http.Json;
using ProyectoPrograCliente.Models;

public class MonsterService
{
    private readonly HttpClient _httpClient;

    public MonsterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Monster>> GetMonstersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Monster>>("") ?? new();
    }

    public async Task<Monster?> GetMonsterAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Monster>($"{id}");
    }

    public async Task<bool> AddMonsterAsync(Monster monster)
    {
        var response = await _httpClient.PostAsJsonAsync("", monster);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateMonsterAsync(Monster monster)
    {
        var response = await _httpClient.PutAsJsonAsync($"{monster.Id}", monster);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteMonsterAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{id}");
        return response.IsSuccessStatusCode;
    }
}
