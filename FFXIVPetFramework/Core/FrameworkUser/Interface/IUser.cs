using FFXIVClientStructs.FFXIV.Client.Game.Character;
using System;
using System.Collections.Generic;

namespace FFXIVPetFramework;

public unsafe interface IUser : IDisposable
{
    /// <summary>
    /// Username of the user.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Homeworld ID of the user.
    /// </summary>
    ushort Homeworld { get; }

    /// <summary>
    /// Content ID of the user.
    /// </summary>
    ulong ContentID { get; }

    /// <summary>
    /// The index in the Object Table of this user.
    /// </summary>
    ushort ObjectIndex { get; }

    /// <summary>
    /// This is the Entity ID of the user.
    /// </summary>
    uint ShortObjectID { get; }

    /// <summary>
    /// This is the Object ID of the user.
    /// </summary>
    ulong ObjectID { get; }

    /// <summary>
    /// Is true if this player is the local player.
    /// </summary>
    bool IsLocalPlayer { get; }

    /// <summary>
    /// A list with all pets this user owns.
    /// </summary>
    List<IPet> Pets { get; }

    /// <summary>
    /// The address of the user.
    /// </summary>
    public nint Address { get; }

    /// <summary>
    /// The BattleChara pointer of the user.
    /// </summary>
    public BattleChara* BattleChara { get; }
}
