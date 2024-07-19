using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.Interop;
using FFXIVPetFramework;
using System.Collections.Generic;

namespace FFXIVPetFramework;

public interface IFrameworkUser : IUser
{
    void Set(Pointer<BattleChara> pointer);
    void CalculateBattlepets(in List<Pointer<BattleChara>> pets);
}
