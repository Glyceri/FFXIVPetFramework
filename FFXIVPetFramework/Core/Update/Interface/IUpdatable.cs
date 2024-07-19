using Dalamud.Plugin.Services;

namespace FFXIVPetFramework.Core.Update.Interface;

internal interface IUpdatable
{
    void OnUpdate(IFramework framework);
}
