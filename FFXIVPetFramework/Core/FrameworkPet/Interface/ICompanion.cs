using FFXIVClientStructs.FFXIV.Client.Game.Character;

namespace FFXIVPetFramework;

public unsafe interface ICompanion : IPet
{
    /// <summary>
    /// The Companion pointer of this pet.
    /// </summary>
    Companion* Companion { get; }
}
