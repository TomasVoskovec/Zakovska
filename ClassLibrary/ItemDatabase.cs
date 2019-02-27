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
            database.CreateTableAsync<Subject>().Wait();
        }

        public async Task<List<Mark>> GetMarksAsync()
        {
            return await database.Table<Mark>().ToListAsync();
        }

        public async Task<List<Subject>> GetSubjectsAsync()
        {
            return await database.Table<Subject>().ToListAsync();
        }

        public Task<List<Mark>> GetMarksNotDoneAsync()
        {
            return database.QueryAsync<Mark>("SELECT * FROM [Mark] WHERE [Done] = 0");
        }

        public Task<List<Subject>> GetSubjectsById(int id)
        {
            return database.QueryAsync<Subject>("SELECT * FROM [Subject] WHERE [Id] = {0}", id);
        }

        public Task<List<Subject>> GetSubjectsByName(string name)
        {
            return database.QueryAsync<Subject>("SELECT * FROM [Subject] WHERE [Name] = {0}", name);
        }

        public async Task<Mark> GetItemAsync(int id)
        {
            return await database.Table<Mark>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveMarkAsync(Mark item)
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

        public Task<int> SaveSubjectAsync(Subject item)
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
