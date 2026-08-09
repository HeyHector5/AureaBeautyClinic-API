namespace AureaBeautyClinic.Shared.Constants
{
    /// <summary>
    /// Nombres de los roles del sistema. Se resuelven por NOMBRE y no por Id:
    /// los RoleId son IDENTITY y varían entre entornos según el orden de inserción.
    /// </summary>
    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";
    }
}
