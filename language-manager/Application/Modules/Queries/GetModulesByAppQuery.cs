using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Modules.Queries;

public record GetModulesByAppQuery(string AppId) : IRequest<Result<IEnumerable<ModuleDto>>>;

public class GetModulesByAppQueryHandler : IRequestHandler<GetModulesByAppQuery, Result<IEnumerable<ModuleDto>>>
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IAppRepository _appRepository;

    public GetModulesByAppQueryHandler(IModuleRepository moduleRepository, IAppRepository appRepository)
    {
        _moduleRepository = moduleRepository;
        _appRepository = appRepository;
    }

    public async Task<Result<IEnumerable<ModuleDto>>> Handle(GetModulesByAppQuery request, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);
        if (app == null)
        {
            return Result<IEnumerable<ModuleDto>>.NotFound("App not found");
        }

        var modules = await _moduleRepository.GetByAppIdAsync(request.AppId, cancellationToken);
        var dtos = modules.Select(m => new ModuleDto(m.ModuleId, m.AppId, m.ModuleKey, m.Name));
        return Result<IEnumerable<ModuleDto>>.Success(dtos);
    }
}
