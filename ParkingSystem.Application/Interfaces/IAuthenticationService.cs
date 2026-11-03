

namespace ParkingSystem.Application.Interfaces
{
    public interface IAuthenticationService
    {
        void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
