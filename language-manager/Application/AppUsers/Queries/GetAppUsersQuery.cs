using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.AppUsers.Queries;

public record GetAppUsersQuery(string AppId) : IRequest<Result<IEnumerable<AppUserDto>>>;

public class GetAppUsersQueryHandler : IRequestHandler<GetAppUsersQuery, Result<IEnumerable<AppUserDto>>>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAppRepository _appRepository;

    public GetAppUsersQueryHandler(
        IAppUserRepository appUserRepository,
        IUserRepository userRepository,
        IAppRepository appRepository)
    {
        _appUserRepository = appUserRepository;
        _userRepository = userRepository;
        _appRepository = appRepository;
    }

    public async Task<Result<IEnumerable<AppUserDto>>> Handle(GetAppUsersQuery request, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);
        if (app == null)
        {
            return Result<IEnumerable<AppUserDto>>.NotFound("App not found");
        }

        var appUsers = await _appUserRepository.GetByAppIdAsync(request.AppId, cancellationToken);
        var userIds = appUsers.Select(au => au.UserId).Distinct().ToList();

        var users = await _userRepository.FindAsync(u => userIds.Contains(u.UserId), cancellationToken);
        var userDict = users.ToDictionary(u => u.UserId);

        var dtos = appUsers.Select(au =>
        {
            UserDto? userDto = null;
            if (userDict.TryGetValue(au.UserId, out var user))
            {
                userDto = new UserDto(user.UserId, user.Username, user.Email);
            }
            return new AppUserDto(au.UserId, au.AppId, au.Role, userDto, null);
        });

        return Result<IEnumerable<AppUserDto>>.Success(dtos);
    }
}
