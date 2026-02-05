using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using MediatR;

namespace language_manager.Application.Modules.Commands;

public record UpdateModuleCommand(string ModuleId, string? ModuleKey, string? Name) : IRequest<Result<ModuleDto>>;

public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, Result<ModuleDto>>
{
    private readonly IModuleRepository _moduleRepository;

    public UpdateModuleCommandHandler(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<Result<ModuleDto>> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetByIdAsync(request.ModuleId, cancellationToken);

        if (module == null)
        {
            return Result<ModuleDto>.NotFound("Module not found");
        }

        if (!string.IsNullOrEmpty(request.ModuleKey) && request.ModuleKey != module.ModuleKey)
        {
            var existingModule = await _moduleRepository.GetByModuleKeyAsync(module.AppId, request.ModuleKey, cancellationToken);
            if (existingModule != null)
            {
                return Result<ModuleDto>.Conflict("A module with this key already exists in this app");
            }
            module.ModuleKey = request.ModuleKey;
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            module.Name = request.Name;
        }

        await _moduleRepository.UpdateAsync(module.ModuleId, module, cancellationToken);

        var dto = new ModuleDto(module.ModuleId, module.AppId, module.ModuleKey, module.Name);
        return Result<ModuleDto>.Success(dto);
    }
}
