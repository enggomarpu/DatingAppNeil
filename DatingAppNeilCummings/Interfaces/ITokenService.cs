using DatingAppNeilCummings.Entities;

namespace DotNetCoreIdentity.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}