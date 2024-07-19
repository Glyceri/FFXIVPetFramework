using FFXIVClientStructs.FFXIV.Client.Game.Character;

namespace FFXIVPetFramework;

public unsafe interface IBattlePet : IPet
{
    /// <summary>
    /// The BattleChara pointer of this pet.
    /// </summary>
    BattleChara* BattlePet { get; }
}
