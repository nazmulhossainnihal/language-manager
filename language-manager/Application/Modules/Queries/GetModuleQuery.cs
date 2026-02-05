using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Modules.Queries;

public record GetModuleQuery(string ModuleId) : IRequest<Result<ModuleDto>>;

public class GetModuleQueryHandler : IRequestHandler<GetModuleQuery, Result<ModuleDto>>
{
    private readonly IModuleRepository _moduleRepository;

    public GetModuleQueryHandler(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<Result<ModuleDto>> Handle(GetModuleQuery request, CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetByIdAsync(request.ModuleId, cancellationToken);

        if (module == null)
        {
            return Result<ModuleDto>.NotFound("Module not found");
        }

        var dto = new ModuleDto(module.ModuleId, module.AppId, module.ModuleKey, module.Name);
        return Result<ModuleDto>.Success(dto);
    }
}
