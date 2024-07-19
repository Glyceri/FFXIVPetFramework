using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.Interop;

namespace FFXIVPetFramework.Core.FrameworkBuilder;

internal unsafe class BasicPetBuilder : IPetBuilder
{
    public FrameworkBattlePet GetBattlePet(in Pointer<BattleChara> battlePet, in IUser owner) => new FrameworkBattlePet(battlePet, owner);
    public FrameworkCompanion GetCompanion(in Pointer<Companion> companion, in IUser owner) => new FrameworkCompanion(companion, owner);
}
