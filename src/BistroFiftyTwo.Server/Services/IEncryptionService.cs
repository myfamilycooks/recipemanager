namespace BistroFiftyTwo.Server.Services
{
    public interface IEncryptionService
    {
        string GenerateSalt();
        string SlowOneWayHash(string clearText, byte[] salt);
        string SlowOneWayHash(string clearText, string salt);
    }
}