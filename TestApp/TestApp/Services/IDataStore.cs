using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public interface IDataStore<T>
    {
        int SaveItem(T item);
        int UpdateItem(T item);
        int DeleteItem(T item);
        T GetItem(int id);
        IEnumerable<T> GetItems();
    }
}
