using FFXIVPetFramework;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.Interop;
using System.Collections.Generic;

namespace FFXIVPetFramework;

public unsafe class User : IFrameworkUser
{
    public string Name { get; }
    public ushort Homeworld { get; }
    public ulong ContentID { get; }

    public ushort ObjectIndex { get; }
    public uint ShortObjectID { get; private set; }
    public ulong ObjectID { get; private set; }

    public bool IsLocalPlayer { get; private set; }

    public nint Address { get; private set; }
    public BattleChara* BattleChara { get; private set; }

    public List<IPet> Pets { get; } = [];

    readonly IPetBuilder PetBuilder;

    public User(in Pointer<BattleChara> battleChara, in IPetBuilder petBuilder)
    {
        PetBuilder = petBuilder;

        BattleChara = battleChara;
        Address = (nint)BattleChara;

        Name = BattleChara->NameString;
        Homeworld = BattleChara->HomeWorld;
        ContentID = BattleChara->ContentId;

        ObjectIndex = BattleChara->ObjectIndex;
        ShortObjectID = BattleChara->GetGameObjectId().ObjectId;
        ObjectID = BattleChara->GetGameObjectId();

        IsLocalPlayer = ObjectIndex == 0;
    }

    public void Set(Pointer<BattleChara> pointer)
    {
        Reset();
        BattleChara = pointer;
        Address = (nint)BattleChara;

        if (pointer == null) return;

        SetCompanion(pointer);
    }

    void SetCompanion(Pointer<BattleChara> pointer)
    {
        Companion* companion = pointer.Value->CompanionData.CompanionObject;
        if (companion == null) return;

        IPet? pet = FindPet(in companion->Character);
        if (pet == null)
        {
            CreateCompanion(companion);
        }
        else
        {
            pet.Update((nint)companion);
        }
    }

    IPet? FindPet(in Character character)
    {
        int petCount = Pets.Count;

        for (int i = 0; i < petCount; i++)
        {
            IPet pet = Pets[i];
            if (!pet.Compare(in character)) continue;

            return pet;
        }

        return null;
    }

    void Reset()
    {
        int petCount = Pets.Count;

        for (int i = 0; i < petCount; i++)
        {
            IPet pet = Pets[i];
            if (!pet.Marked)
            {
                Pets.RemoveAt(i);
            }

            pet.Marked = false;
        }
    }

    public void CalculateBattlepets(in List<Pointer<BattleChara>> pets)
    {
        for (int i = pets.Count - 1; i >= 0; i--)
        {
            Pointer<BattleChara> bChara = pets[i];
            if (bChara == null) continue;
            if (bChara.Value->OwnerId != ShortObjectID) continue;

            BattleChara* battlePet = bChara.Value;
            if (battlePet == null) continue;

            pets.RemoveAt(i);

            IPet? storedPet = FindPet(in battlePet->Character);
            if (storedPet == null)
            {
                CreateBattlePet(battlePet);
            }
            else
            {
                storedPet.Update((nint)battlePet);
            }
        }
    }

    void CreateCompanion(Companion* companion)
    {
        Pets.Add(PetBuilder.GetCompanion(companion, this));
    }

    void CreateBattlePet(BattleChara* battlePet)
    {
        Pets.Add(PetBuilder.GetBattlePet(battlePet, this));
    }

    public virtual void Dispose() { }
}
