using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Exceptions;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using AureaBeautyClinic.Shared.Interfaces.IServices;
using BCrypt.Net;

namespace AureaBeautyClinic.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponse> RegisterAsync(int RoleId, string name, string lastName, string email, string password, string? phone)
        {
            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing is not null)
                throw new EmailAlreadyRegisteredException(email);

            var user = new User
            {
                RoleId = RoleId,
                FirstName = name,
                LastName = lastName,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Phone = phone,
                Registered = DateTime.Now,
                IsActive = true
            };

            var created = await _userRepository.CreateUserAsync(user);
            var fullUser = await _userRepository.GetByIdAsync(created.UserId)
                ?? throw new InvalidOperationException("User was created but could not be retrieved.");

            var (token, expiresAt) = _jwtTokenGenerator.Generate(fullUser);
            return new AuthResponse(token, expiresAt, fullUser.ToDto());
        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null || !user.IsActive)
                throw new AuthenticationException("Invalid email or password.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new AuthenticationException("Invalid email or password.");

            var (token, expiresAt) = _jwtTokenGenerator.Generate(user);
            return new AuthResponse(token, expiresAt, user.ToDto());
        }
    }
}
