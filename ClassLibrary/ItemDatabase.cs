using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ItemDatabase
    {
        SQLiteAsyncConnection database;
        public ItemDatabase(string dbPath)
        {
            this.database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Mark>().Wait();
        }

        public async Task<List<Mark>> GetItemsAsync()
        {
            return await database.Table<Mark>().ToListAsync();
        }

        public Task<List<Mark>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Mark>("SELECT * FROM [Mark] WHERE [Done] = 0");
        }

        public async Task<Mark> GetItemAsync(int id)
        {
            return await database.Table<Mark>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Mark item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Mark item)
        {
            return database.DeleteAsync(item);
        }
    }
}
