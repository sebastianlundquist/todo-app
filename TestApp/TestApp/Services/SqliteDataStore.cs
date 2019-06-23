using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Services
{
    public class SqliteDataStore : IDataStore<Item>
    {
        readonly SQLiteConnection database;

        public SqliteDataStore(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Item>();
        }
        public int SaveItem(Item item)
        {
            if (item.Id != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }

        public int UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }

        public int DeleteItem(Item item)
        {
            return database.Delete(item);
        }

        public Item GetItem(int id)
        {
            return database.Table<Item>().Where(i => i.Id == id).FirstOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return database.Table<Item>().ToList();
        }
    }
}
