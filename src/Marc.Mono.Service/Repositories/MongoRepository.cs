using MongoDB.Driver;
using Marc.Mono.Service.Entities;
using System.Linq.Expressions;

namespace Marc.Mono.Service.Repositories;

    public class MongoRepository<T>(IMongoDatabase database, string collectionName) : IRepository<T> where T:IEntity //I have used a primary constructor here it seems
    {
        private readonly IMongoCollection<T> _dbCollection = database.GetCollection<T>(collectionName);

          private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public async Task<int> CountAsync(string? filter)
        {
            return (int)await _dbCollection.CountDocumentsAsync(filter);
        }

        public async Task CreateAsync(T item)//methods create exactly one sport in collection
        {
        ArgumentNullException.ThrowIfNull(item);//make sure sport is not null before attempting to save

        await _dbCollection.InsertOneAsync(item);//now enter sport because it exists
        }

        public async Task DeleteAsync(Guid id)//method creates exactly one sport in collection
        {

            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);//filters the object whose Id matched the passed id
            await _dbCollection.DeleteOneAsync(filter);//executes the deletion of the object filtered
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)//returns mutilple or many objects matching a specific filter
        {
           return await _dbCollection.Find(filter).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()//returns all documents in collection unfiltered
        {
            return await _dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<T?> GetAsync(Guid id)//returns a specific document on our collection based on ID
        {
            return await _dbCollection.Find(filterBuilder.Eq(entity => entity.Id, id)).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T updatedItem)//used to update a specific document on our collection based on ID
        {

        ArgumentNullException.ThrowIfNull(updatedItem);

        FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id,updatedItem.Id) ;
            await _dbCollection.ReplaceOneAsync(filter ,updatedItem);
        }
    }

