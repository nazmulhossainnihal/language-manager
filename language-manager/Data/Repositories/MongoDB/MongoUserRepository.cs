using language_manager.Data.Context;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MongoDB.Driver;

namespace language_manager.Data.Repositories.MongoDB;

/// <summary>
/// MongoDB implementation of the User repository.
/// </summary>
public class MongoUserRepository : MongoRepository<User>, IUserRepository
{
    public MongoUserRepository(MongoDbContext context)
        : base(context.Users, nameof(User.UserId))
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(u => u.Username == username).FirstOrDefaultAsync(cancellationToken);
    }
}
