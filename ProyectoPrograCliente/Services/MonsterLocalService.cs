using ProyectoPrograCliente.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MonsterLocalService
{
    private readonly SQLiteAsyncConnection _database;

    public MonsterLocalService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Monster>().Wait();
    }

    public Task<List<Monster>> GetMonstersAsync()
    {
        return _database.Table<Monster>().ToListAsync();
    }

    public Task<int> SaveMonsterAsync(Monster monster)
    {
        return _database.InsertOrReplaceAsync(monster);
    }

    public Task<int> DeleteMonsterAsync(Monster monster)
    {
        return _database.DeleteAsync(monster);
    }

    public Task<int> DeleteAllAsync()
    {
        return _database.DeleteAllAsync<Monster>();
    }
}
