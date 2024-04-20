using MongoDB.Driver;
using Marc.Mono.Service.Entities;
using System.Linq.Expressions;

namespace Marc.Mono.Service.Repositories;

    public class MongoRepository<T> : ISportsRepository 
    {
        private readonly IMongoCollection<Sport> _sportsCollection;

          private readonly FilterDefinitionBuilder<Sport> filterBuilder = Builders<Sport>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _sportsCollection = database.GetCollection<Sport>(collectionName);
        }

        public async Task<int> CountAsync(string? filter)
        {
            return (int)await _sportsCollection.CountDocumentsAsync(filter);
        }

        public async Task CreateAsync(Sport sport)//methods create exactly one sport in collection
        {
            if(sport is null) throw new ArgumentNullException(nameof(sport));//make sure sport is not null before attempting to save

            await _sportsCollection.InsertOneAsync(sport);//now enter sport because it exists
        }

        public async Task DeleteAsync(int id)//method creates exactly one sport in collection
        {

            FilterDefinition<Sport> filter = filterBuilder.Eq(entity => entity.Id, id);//filters the object whose Id matched the passed id
            await _sportsCollection.DeleteOneAsync(filter);//executes the deletion of the object filtered
        }

        public async Task<IReadOnlyCollection<Sport>> GetAllAsync(Expression<Func<Sport, bool>> filter)//returns mutilple or many objects matching a specific filter
        {
           return await _sportsCollection.Find(filter).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Sport>> GetAllAsync()//returns all documents in collection unfiltered
        {
            return await _sportsCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Sport?> GetAsync(int id)//returns a specific document on our collection based on ID
        {
            return await _sportsCollection.Find(filterBuilder.Eq(entity => entity.Id, id)).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Sport updatedsport)//used to update a specific document on our collection based on ID
        {
    
            if(updatedsport is null) throw new ArgumentNullException(nameof(updatedsport));

            FilterDefinition<Sport> filter = filterBuilder.Eq(entity => entity.Id,updatedsport.Id) ;
            await _sportsCollection.ReplaceOneAsync(filter ,updatedsport);
        }
    }

