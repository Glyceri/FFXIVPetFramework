using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.Interop;

namespace FFXIVPetFramework.Core.FrameworkBuilder;

internal class BasicUserBuilder : IUserBuilder
{
    readonly IPetBuilder PetBuilder;

    public BasicUserBuilder(in IPetBuilder petBuilder)
    {
        PetBuilder = petBuilder;
    }

    public User CreateUser(in Pointer<BattleChara> battleChara) => new User(in battleChara, in PetBuilder);
}
