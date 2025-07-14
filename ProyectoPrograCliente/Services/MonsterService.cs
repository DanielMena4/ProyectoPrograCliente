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
        try
        {
            var monsters = await _httpClient.GetFromJsonAsync<List<Monster>>("");
            return monsters ?? new List<Monster>();
        }
        catch
        {
            return new List<Monster>();
        }
    }

    public async Task<Monster?> GetMonsterAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Monster>($"{id}");
        }
        catch
        {
            return null;
        }
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
