using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.Interop;

namespace FFXIVPetFramework;

public interface IUserBuilder
{
    User CreateUser(in Pointer<BattleChara> battleChara);
}
