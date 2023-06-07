using MnimalApiCatalogo.Models;

namespace MnimalApiCatalogo.Services
{
    public interface ITokenService
    {
        string GerarToken(string key, string issuer, string audience, UserModel user);
    }
}
