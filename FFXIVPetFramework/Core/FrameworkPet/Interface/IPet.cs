using FFXIVClientStructs.FFXIV.Client.Game.Character;

namespace FFXIVPetFramework;

public unsafe interface IPet
{
    /// <summary>
    /// Username of the pet.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The skeleton of this pet.
    /// </summary>
    int SkeletonID { get; }

    /// <summary>
    /// The type of this pet.
    /// </summary>
    byte PetType { get; }

    /// <summary>
    /// The index in the Object Table of the pet.
    /// </summary>
    ushort ObjectIndex { get; }

    /// <summary>
    /// This is the Entity ID of the pet.
    /// </summary>
    uint EntityID { get; }

    /// <summary>
    /// This is the Object ID of the pet.
    /// </summary>
    ulong ObjectID { get; }

    /// <summary>
    /// The address of the pet.
    /// </summary>
    nint Address { get; }

    /// <summary>
    /// The Character pointer of the pet.
    /// </summary>
    Character* Character { get; }

    /// <summary>
    /// The owner of this pet.
    /// </summary>
    IUser Owner { get; }

    /// <summary>
    /// A variable that counts up every frame.
    /// </summary>
    ulong Lifetime { get; }

    /// <summary>
    ///  Dont touch this value if you don't know what you are doing
    /// </summary>
    bool Marked { get; set; }

    /// <summary>
    /// Called by the framework. (Or if you know better by you).
    /// It sets the internal pointer of this pet.
    /// If you overwrite it is HIGHLY recommended you call base.Update
    /// </summary>
    /// <param name="pointer">Pointer to the pet.</param>
    void Update(nint pointer);

    /// <summary>
    /// Compare if the pet equals the given character.
    /// </summary>
    /// <param name="character">Character to compare.</param>
    /// <returns>true if it compared.</returns>
    bool Compare(in Character character);
}
