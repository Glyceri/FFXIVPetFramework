using FFXIVClientStructs.FFXIV.Client.Game.Character;

namespace FFXIVPetFramework;

public unsafe class FrameworkBattlePet : Pet, IBattlePet
{
    public BattleChara* BattlePet { get; private set; }

    public FrameworkBattlePet(BattleChara* character, in IUser owner) : base(&character->Character, owner)
    {
        BattlePet = character;

        SkeletonID = -SkeletonID;
    }

    public override void Update(nint pointer)
    {
        base.Update(pointer);
        BattlePet = (BattleChara*)pointer;
    }
}
