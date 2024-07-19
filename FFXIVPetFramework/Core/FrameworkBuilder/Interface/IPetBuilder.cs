using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.Interop;

namespace FFXIVPetFramework;

public interface IPetBuilder
{
    FrameworkBattlePet GetBattlePet(in Pointer<BattleChara> battlePet, in IUser owner);
    FrameworkCompanion GetCompanion(in Pointer<Companion> companion, in IUser owner);
}
