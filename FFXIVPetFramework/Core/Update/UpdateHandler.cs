using Dalamud.Plugin.Services;
using FFXIVPetFramework.Core.Update.Elements;
using FFXIVPetFramework.Core.Update.Interface;
using System;
using System.Collections.Generic;

namespace FFXIVPetFramework.Core.Update;

internal class UpdateHandler : IDisposable
{
    readonly IFramework Framework;
    readonly IUserList UserList;
    readonly IUserBuilder UserBuilder;
    readonly IPetBuilder PetBuilder;

    readonly List<IUpdatable> Updatables = [];

    public UpdateHandler(in IFramework framework, in IUserList userList, in IUserBuilder userBuilder, in IPetBuilder petbuilder)
    {
        Framework = framework;
        UserBuilder = userBuilder;
        PetBuilder = petbuilder;
        UserList = userList;

        Framework.Update += OnUpdate;

        Register();
    }

    void Register()
    {
        Register(new MainLoop(in UserList, in UserBuilder, in PetBuilder));
    }

    void Register(IUpdatable updatable)
    {
        Updatables.Add(updatable);
    }

    void OnUpdate(IFramework framework)
    {
        int updatableCount = Updatables.Count;

        for (int i = 0; i < updatableCount; i++)
        {
            Updatables[i].OnUpdate(framework);
        }
    }

    public void Dispose()
    {
        Framework.Update -= OnUpdate;
        Updatables.Clear();
    }
}
