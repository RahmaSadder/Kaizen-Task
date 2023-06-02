
using CoreRepository.Models;

namespace HappyCompany_API.Helper
{
    public interface IJWT
    {
        string GenerateToken(User user);
    }
}
