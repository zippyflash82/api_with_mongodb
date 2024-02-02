


using api_with_mongodb.Data;
using api_with_mongodb.Models;

namespace Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(LoginDto loginDto);
        
    }
}