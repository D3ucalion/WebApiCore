using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;
using WebApiCore.Models.TodoApi.Models;

namespace WebApiCore.Models
{
    public class TodoRepository : ITodoRepository
    {
        private static ConcurrentDictionary<BsonString, TodoItem> _todos =
              new ConcurrentDictionary<BsonString, TodoItem>();

        readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        public TodoRepository()
        {
            _client = new MongoClient("mongodb://localhost:3001");
            _db = _client.GetDatabase("meteor");
            //Add(new TodoItem { Name = "Todo Item0" });
        }

        public async Task<List<TodoItem>> GetAll()
        {
            return await _db.GetCollection<TodoItem>("TodoItems").Find(new BsonDocument()).ToListAsync();
        }

        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _db.GetCollection<TodoItem>("TodoItems").InsertOne(item);
            //_todos[item.Key] = item;
        }

        public async Task<TodoItem> Find(string key)
        {
            try
            {
                var filter = Builders<TodoItem>.Filter.Eq("Key", key);
                return await _db.GetCollection<TodoItem>("TodoItems").Find(filter).FirstOrDefaultAsync();
            }
            catch (InvalidOperationException ex)
            {
                
                throw ex;
            }
            
        }

        public void Remove(string key)
        {
            var filter = Builders<TodoItem>.Filter.Eq("Key", key);
            _db.GetCollection<TodoItem>("TodoItems").DeleteOne(filter); 
        }

        public void Update(TodoItem item)
        {
            var filter = Builders<TodoItem>.Filter.Eq("Key", item.Key);
            var update = Builders<TodoItem>.Update.Set("IsComplete", item.IsComplete);
            _db.GetCollection<TodoItem>("TodoItems").UpdateOne(filter, update);
        }
    }
}
