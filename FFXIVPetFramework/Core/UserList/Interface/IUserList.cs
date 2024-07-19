namespace FFXIVPetFramework;

public interface IUserList
{
    /// <summary>
    /// The array with every active PlayerCharater
    /// Entries CAN be NULL!
    /// </summary>
    IUser?[] Users { get; }
}
