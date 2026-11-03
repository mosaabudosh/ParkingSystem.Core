using ParkingSystem.Domain.Domains;


namespace ParkingSystem.Application.Interfaces
{
    public interface ITokenHandler
    {
        User BuildUserTokens(User user);
    }
}
