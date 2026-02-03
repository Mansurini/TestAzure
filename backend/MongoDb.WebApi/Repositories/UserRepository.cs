using MongoDb.WebApi.Documents;
using MongoDB.Driver;

namespace MongoDb.WebApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IMongoDatabase mongoDatabase)
    {
        _collection = mongoDatabase.GetCollection<User>(nameof(User));
    }

    public async Task<User> CreateAsync(User model, CancellationToken cancellationToken = default)
    {
        model.Id = Guid.NewGuid().ToString();
        await _collection.InsertOneAsync(model, cancellationToken: cancellationToken);
        return model;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(int offset, int fetch, CancellationToken cancellationToken = default)
    {
        var filter = Builders<User>.Filter.Empty;

        return await _collection
            .Find(filter)
            .Skip(offset)
            .Limit(fetch)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(model => model.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(string id, User model, CancellationToken cancellationToken = default)
    {
        await _collection.FindOneAndUpdateAsync(
            Builders<User>.Filter.Eq(c => c.Id, id),
            Builders<User>.Update
                .Set(c => c.Name, model.Name)
                .Set(c => c.Username, model.Username)
                .Set(c => c.Email, model.Email)
                .Set(c => c.PhoneNumber, model.PhoneNumber),
            new FindOneAndUpdateOptions<User> { ReturnDocument = ReturnDocument.After },
            cancellationToken);
    }
}
