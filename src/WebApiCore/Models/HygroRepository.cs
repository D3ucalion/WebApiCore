using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApiCore.Models.HygroApi.Models;

namespace WebApiCore.Models
{
    public class HygroRepository : IHygroRepository
    {
        private static ConcurrentDictionary<BsonString, HygroItem> _todos =
              new ConcurrentDictionary<BsonString, HygroItem>();

        readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        public HygroRepository()
        {
            _client = new MongoClient("mongodb://localhost:3001");
            _db = _client.GetDatabase("meteor");
        }

        public async Task<List<HygroItem>> GetAll()
        {
            return await _db.GetCollection<HygroItem>("HygroItems").Find(new BsonDocument()).ToListAsync();
        }

        public void Add(HygroItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            item.TimeStamp = DateTime.Now;
            _db.GetCollection<HygroItem>("HygroItems").InsertOne(item);
        }

        public async Task<HygroItem> Find()
        {
            try
            {
                SortDefinition<HygroItem> sort = Builders<HygroItem>.Sort.Descending("_id");
                return await _db.GetCollection<HygroItem>("HygroItems").Find(new BsonDocument()).Sort(sort).FirstOrDefaultAsync();
            }
            catch (InvalidOperationException ex)
            {

                throw ex;
            }

        }
    }
}
