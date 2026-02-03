using MongoDb.WebApi.Documents;

namespace MongoDb.WebApi.Repositories;

public interface IUserRepository
{
    Task<User> CreateAsync(User model, CancellationToken cancellationToken = default);
    Task UpdateAsync(string id, User model, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAllAsync(int offset, int fetch, CancellationToken cancellationToken = default);
}
