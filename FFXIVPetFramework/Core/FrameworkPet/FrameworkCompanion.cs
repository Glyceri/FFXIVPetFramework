using FFXIVClientStructs.FFXIV.Client.Game.Character;

namespace FFXIVPetFramework;

public unsafe class FrameworkCompanion : Pet, ICompanion
{
    public unsafe Companion* Companion { get; private set; }

    public FrameworkCompanion(Companion* companion, in IUser owner) : base(&companion->Character, owner)
    {
        Companion = companion;
    }

    public override void Update(nint pointer)
    {
        base.Update(pointer);
        Companion = (Companion*)pointer;
    }
}
