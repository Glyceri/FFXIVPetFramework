using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVPetFramework.Core.FrameworkBuilder;
using FFXIVPetFramework.Core.Update;
using FFXIVPetFramework.Core.UserList;
using System;

namespace FFXIVPetFramework;

public class PetFramework : IDisposable
{
    readonly DalamudServices DalamudServices;
    readonly UpdateHandler UpdateHandler;

    public PetFramework(in IDalamudPluginInterface pluginInterface, out IUserList UserList) : this (pluginInterface, null, null, out UserList) { }
    public PetFramework(in IDalamudPluginInterface pluginInterface, IUserBuilder? userBuilder, IPetBuilder? petBuilder, out IUserList UserList)
    {
        DalamudServices = pluginInterface.Create<DalamudServices>()!;
        DalamudServices.PluginInterface = pluginInterface;

        petBuilder  ??= new BasicPetBuilder();
        userBuilder ??= new BasicUserBuilder(in petBuilder);

        UserList = new UserList();
        UpdateHandler = new UpdateHandler(DalamudServices.Framework, in UserList, in userBuilder, in petBuilder);
    }

    public void Dispose()
    {
        UpdateHandler.Dispose();
    }
}

internal class DalamudServices
{
    [PluginService] public IFramework Framework { get; } = null!;
    [PluginService] public IPluginLog PluginLog { get; } = null!;

    public IDalamudPluginInterface PluginInterface { get; set; } = null!;
}