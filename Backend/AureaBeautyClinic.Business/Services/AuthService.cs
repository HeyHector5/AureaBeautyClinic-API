using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.Constants;
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
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponse> RegisterAsync(string name, string lastName, string email, string password, string? phone)
        {
            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing is not null)
                throw new EmailAlreadyRegisteredException(email);

            // El registro público SIEMPRE crea un paciente. El rol se busca por nombre
            // porque los RoleId son IDENTITY y difieren entre entornos; además, aceptar
            // un roleId del cliente permitiría que cualquiera se auto-asignara Admin.
            var patientRole = await _roleRepository.GetByNameAsync(RoleNames.Patient)
                ?? throw new InvalidOperationException(
                    $"El rol '{RoleNames.Patient}' no existe en la base de datos. " +
                    "Ejecuta Database/SeedRoles.sql antes de registrar usuarios.");

            var user = new User
            {
                RoleId = patientRole.RoleId,
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
