using language_manager.Data.Context;
using language_manager.Model.Domain;
using language_manager.Services.Interfaces;

namespace language_manager.Data.Seeds.Seeders;

public class UserSeeder : ISeeder
{
    private readonly MongoDbContext _context;
    private readonly IPasswordService _passwordService;

    public UserSeeder(MongoDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public string Name => "002_UserSeeder";
    public int Order => 2;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var users = GetUsers();
        await _context.Users.InsertManyAsync(users, cancellationToken: cancellationToken);
    }

    private List<User> GetUsers()
    {
        return new List<User>
        {
            new()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = "admin@language-manager",
                Password = _passwordService.HashPassword("G$af5223Xpy")
            }
        };
    }
}
