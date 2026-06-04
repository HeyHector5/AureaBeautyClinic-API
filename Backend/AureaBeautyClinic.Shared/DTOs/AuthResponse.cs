namespace AureaBeautyClinic.Shared.DTOs
{
    public record AuthResponse(
        string token,
        DateTime expiresAt,
        UserDTO user
    );
}
