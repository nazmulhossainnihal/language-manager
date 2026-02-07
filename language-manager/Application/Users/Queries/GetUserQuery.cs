using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Users.Queries;

public record GetUserQuery(string UserId) : IRequest<Result<UserDto>>;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Result<UserDto>.NotFound("User not found");
        }

        var userDto = new UserDto(user.UserId, user.Email);
        return Result<UserDto>.Success(userDto);
    }
}
