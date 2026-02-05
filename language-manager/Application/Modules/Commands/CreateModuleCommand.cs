using language_manager.Application.Common;
using language_manager.Application.DTOs;
using language_manager.Data.Repositories.Interfaces;
using language_manager.Model.Domain;
using MediatR;

namespace language_manager.Application.Modules.Commands;

public record CreateModuleCommand(string AppId, string ModuleKey, string Name) : IRequest<Result<ModuleDto>>;

public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, Result<ModuleDto>>
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IAppRepository _appRepository;

    public CreateModuleCommandHandler(IModuleRepository moduleRepository, IAppRepository appRepository)
    {
        _moduleRepository = moduleRepository;
        _appRepository = appRepository;
    }

    public async Task<Result<ModuleDto>> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var app = await _appRepository.GetByIdAsync(request.AppId, cancellationToken);
        if (app == null)
        {
            return Result<ModuleDto>.NotFound("App not found");
        }

        var existingModule = await _moduleRepository.GetByModuleKeyAsync(request.AppId, request.ModuleKey, cancellationToken);
        if (existingModule != null)
        {
            return Result<ModuleDto>.Conflict("A module with this key already exists in this app");
        }

        var module = new Module
        {
            ModuleId = Guid.NewGuid().ToString(),
            AppId = request.AppId,
            ModuleKey = request.ModuleKey,
            Name = request.Name
        };

        await _moduleRepository.AddAsync(module, cancellationToken);

        var dto = new ModuleDto(module.ModuleId, module.AppId, module.ModuleKey, module.Name);
        return Result<ModuleDto>.Success(dto, 201);
    }
}
