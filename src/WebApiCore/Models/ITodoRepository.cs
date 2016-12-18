using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApiCore.Models.TodoApi.Models;

namespace WebApiCore.Models
{
    public interface ITodoRepository
    {
        void Add(TodoItem item);
        Task<List<TodoItem>> GetAll();
        Task<TodoItem> Find(string key);
        void Remove(string key);
        void Update(TodoItem item);
    }
}
