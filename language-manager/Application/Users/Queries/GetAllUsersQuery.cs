using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Users.Queries;

public record GetAllUsersQuery : IRequest<Result<IEnumerable<UserDto>>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        var userDtos = users.Select(u => new UserDto(u.UserId, u.Email));
        return Result<IEnumerable<UserDto>>.Success(userDtos);
    }
}
