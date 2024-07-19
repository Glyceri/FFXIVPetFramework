using FFXIVClientStructs.FFXIV.Client.Game.Character;

namespace FFXIVPetFramework;

public unsafe abstract class Pet : IPet
{
    public string Name { get; }

    public int SkeletonID { get; init; }
    public byte PetType { get; }

    public ushort ObjectIndex { get; }
    public uint EntityID { get; }
    public ulong ObjectID { get; }

    public ulong Lifetime { get; private set; }

    public nint Address { get; private set; }
    public unsafe Character* Character { get; private set; }

    public IUser Owner { get; }
    public bool Marked { get; set; }

    public Pet(Character* character, in IUser owner)
    {
        Owner = owner;

        Character = character;
        Address = (nint)Character;

        Name = Character->NameString;

        SkeletonID = Character->CharacterData.ModelCharaId;

        ObjectIndex = Character->ObjectIndex;
        EntityID = Character->EntityId;
        ObjectID = Character->GetGameObjectId();
        PetType = Character->GetGameObjectId().Type;
    }

    public virtual void Update(nint pointer)
    {
        Address = pointer;
        Character = (Character*)pointer;

        Lifetime++;
    }

    public bool Compare(in Character character)
    {
        int skeletonID = character.CharacterData.ModelCharaId;
        ushort index = character.ObjectIndex;
        uint objectId = character.EntityId;

        int ownSkeletonID = SkeletonID;

        if (ownSkeletonID < 0) ownSkeletonID = -ownSkeletonID;

        return ownSkeletonID == skeletonID && index == ObjectIndex && EntityID == objectId;
    }
}
