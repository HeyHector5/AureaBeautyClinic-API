using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IJwtTokenGenerator
    {
        (string Token, DateTime ExpiresAt) Generate(User user);
    }
}
