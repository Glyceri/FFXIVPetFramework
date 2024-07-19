using Dalamud.Plugin.Services;
using FFXIVPetFramework.Core.Update.Interface;
using FFXIVClientStructs.Interop;
using System;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using System.Collections.Generic;
using System.Linq;

namespace FFXIVPetFramework.Core.Update.Elements;

internal class MainLoop : IUpdatable
{
    readonly IUserList UserList;
    readonly IUserBuilder UserBuilder;
    readonly IPetBuilder PetBuilder;

    readonly List<Pointer<BattleChara>> availablePets = new List<Pointer<BattleChara>>(100);

    public MainLoop(in IUserList userList, in IUserBuilder userBuilder, in IPetBuilder petbuilder)
    {
        UserList = userList;
        UserBuilder = userBuilder;
        PetBuilder = petbuilder;
    }

    public unsafe void OnUpdate(IFramework framework)
    {
        Span<Pointer<BattleChara>> charaSpan = CharacterManager.Instance()->BattleCharas;
        int charaSpanLength = charaSpan.Length;

        availablePets.Clear();

        for (int i = 0; i < charaSpanLength; i++)
        {
            Pointer<BattleChara> battleChara = charaSpan[i];
            IFrameworkUser? user = UserList.Users[i] as IFrameworkUser;

            ObjectKind currentObjectKind = ObjectKind.None;
            ulong userContentID = ulong.MaxValue;
            ulong contentID = ulong.MaxValue;

            if (battleChara != null)
            {
                contentID = battleChara.Value->ContentId;
                currentObjectKind = battleChara.Value->GetObjectKind();
            }

            if (user != null) userContentID = user.ContentID;

            if (contentID == ulong.MaxValue || contentID == 0 || userContentID != contentID)
            {
                // Destroy the user
                user?.Dispose();
                UserList.Users[i] = null;
            }

            if (user == null && battleChara != null && currentObjectKind == ObjectKind.Pc)
            {
                // Create a user
                UserList.Users[i] = UserBuilder.CreateUser(in battleChara);
                continue;
            }

            if (currentObjectKind == ObjectKind.BattleNpc) availablePets.Add(battleChara);

            user?.Set(battleChara);
        }

        IFrameworkUser?[] users = UserList.Users.Cast<IFrameworkUser>().ToArray();
        int size = users.Length;
        for (int i = 0; i < size; i++)
        {
            users[i]?.CalculateBattlepets(in availablePets);
        }
    }
}
