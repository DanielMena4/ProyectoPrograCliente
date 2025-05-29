using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoPrograCliente.Models;
using System.Net.Http.Json;

namespace ProyectoPrograCliente.Services
{
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
                return await _httpClient.GetFromJsonAsync<List<Monster>>("api/monsters");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching monsters: {ex.Message}");
                return new List<Monster>();
            }
        }
        public async Task<Monster> GetMonsterByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Monster>($"api/monsters/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching monster with ID {id}: {ex.Message}");
                return null;
            }
        }
        public async Task<Monster> AddMonsterAsync(Monster monster)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/monsters", monster);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Monster>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating monster: {ex.Message}");
                return null;
            }
        }
        public async Task<Monster> UpdateMonsterAsync(Monster monster)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/monsters/{monster.Id}", monster);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Monster>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating monster: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> DeleteMonsterAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/monsters/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting monster with ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
