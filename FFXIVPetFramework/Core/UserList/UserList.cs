namespace FFXIVPetFramework.Core.UserList;

internal class UserList : IUserList
{
    // There are a maximum of 100 users available.
    const int UserArraySize = 100;

    public IUser?[] Users { get; } = new IUser[UserArraySize];
    public IUser? LocalPlayer { get => Users[0]; }
}
