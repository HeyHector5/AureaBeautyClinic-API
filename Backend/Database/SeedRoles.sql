/*
    Datos semilla para la tabla Roles.
    Necesario antes de registrar usuarios: User.RoleId tiene una FK
    que obliga a que el rol exista previamente en esta tabla.
*/

INSERT INTO Roles (Name, Description) VALUES
    ('Admin',   'Administrador del sistema'),
    ('Doctor',  'Médico de la clínica'),
    ('Patient', 'Paciente / cliente');
GO

SELECT RoleId, Name FROM Roles;
GO
