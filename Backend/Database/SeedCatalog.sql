/*
    AureaBeautyClinic - Datos semilla del catálogo (especialidades y equipo médico).

    Necesario para poder probar el flujo completo de reserva de citas:
      /servicios  lee Specialties activas
      /nosotros   muestra el equipo desde Doctors
      /reservar   necesita una Specialty y un Doctor activos

    Requisitos previos:
      1. CreateTables.sql
      2. SeedRoles.sql              (si falta el rol 'Doctor', este script lo crea)
      3. AlterRoles_AddIsActive.sql (sólo en bases creadas antes de ese cambio)

    Idempotente: se puede ejecutar varias veces. Las filas existentes se
    actualizan (especialidad, licencia, biografía, foto) en lugar de duplicarse.

    Contraseña de todas las cuentas de doctor: Doctor1234
    (hash BCrypt generado con BCrypt.Net-Next 4.0.3, el mismo que usa AuthService)

    Fotos: Unsplash, servidas desde su CDN con recorte cuadrado centrado en la
    cara (w=400&h=400&fit=crop&crop=faces). Uso libre, sin atribución requerida.
    Cada URL fue verificada (HTTP 200, image/jpeg) y revisada visualmente para
    confirmar que es un retrato frontal.

    NOTA para sqlcmd: ejecutar con el flag -x. Sin él, sqlcmd interpreta los '$'
    del hash BCrypt como variables y corrompe la contraseña.
*/

------------------------------------------------------------
-- 1. Especialidades / servicios
------------------------------------------------------------
MERGE INTO Specialties AS target
USING (VALUES
    ('Limpieza facial profunda',
     'Exfoliación, extracción e hidratación para dejar tu piel visiblemente renovada.'),
    ('Microneedling',
     'Estimula el colágeno natural para reducir cicatrices, líneas finas y textura irregular.'),
    ('Masaje relajante corporal',
     'Técnicas de liberación muscular y aceites esenciales para aliviar tensión y estrés.'),
    ('Depilación láser',
     'Reducción progresiva y permanente del vello con tecnología de última generación.'),
    ('Peeling químico',
     'Renueva las capas superficiales de la piel para un tono más uniforme y luminoso.'),
    ('Dermatología estética',
     'Diagnóstico y tratamiento de condiciones cutáneas con enfoque médico.'),
    ('Consulta de valoración',
     'Evaluación personalizada con nuestros especialistas para diseñar tu plan de tratamiento.')
) AS source (Name, Description)
ON target.Name = source.Name
WHEN MATCHED THEN
    UPDATE SET Description = source.Description, IsActive = 1
WHEN NOT MATCHED THEN
    INSERT (Name, Description, IsActive)
    VALUES (source.Name, source.Description, 1);
GO

------------------------------------------------------------
-- 2. Cuentas de usuario del equipo médico
------------------------------------------------------------

-- El rol 'Doctor' puede no existir: hay entornos poblados con otro conjunto
-- de roles (User / Admin / Patient). Se crea si hace falta.
IF NOT EXISTS (SELECT 1 FROM Roles WHERE Name = 'Doctor')
BEGIN
    INSERT INTO Roles (Name, Description) VALUES ('Doctor', 'Médico de la clínica');
END

DECLARE @DoctorRoleId INT = (SELECT RoleId FROM Roles WHERE Name = 'Doctor');

MERGE INTO Users AS target
USING (VALUES
    ('Daniela',  'Marte',     'daniela.marte@aureabeautyclinic.com',     '809-555-0101'),
    ('Patricia', 'Núñez',     'patricia.nunez@aureabeautyclinic.com',    '809-555-0102'),
    ('Luis',     'Reyes',     'luis.reyes@aureabeautyclinic.com',        '809-555-0103'),
    ('Ricardo',  'Fernández', 'ricardo.fernandez@aureabeautyclinic.com', '809-555-0104'),
    ('Yamilé',   'Santana',   'yamile.santana@aureabeautyclinic.com',    '809-555-0105'),
    ('Carolina', 'Peña',      'carolina.pena@aureabeautyclinic.com',     '809-555-0106'),
    ('Manuel',   'Guzmán',    'manuel.guzman@aureabeautyclinic.com',     '809-555-0107')
) AS source (FirstName, LastName, Email, Phone)
ON target.Email = source.Email
WHEN MATCHED THEN
    -- Reasigna el teléfono para que coincida con esta versión del script:
    -- ejecuciones anteriores repartían los números de otra forma.
    -- No toca PasswordHash ni RoleId, por si la cuenta ya se usó.
    UPDATE SET Phone = source.Phone, IsActive = 1
WHEN NOT MATCHED THEN
    INSERT (RoleId, FirstName, LastName, Email, PasswordHash, Phone, Registered, IsActive)
    VALUES (
        @DoctorRoleId,
        source.FirstName,
        source.LastName,
        source.Email,
        '$2a$11$1cGcmwTI3JRisGo1Ly07UO2mgB3OoyVNEPkIe4H.Me6Wl4XOAG6cy', -- Doctor1234
        source.Phone,
        SYSDATETIME(),
        1
    );
GO

------------------------------------------------------------
-- 3. Fichas de doctor (enlazan usuario + especialidad + foto)
------------------------------------------------------------
MERGE INTO Doctors AS target
USING (
    SELECT u.UserId, s.SpecialtyId, v.LicenseNumber, v.Biography, v.PhotoURL
    FROM (VALUES
        ('daniela.marte@aureabeautyclinic.com', 'Dermatología estética', 'EX-10234',
         'Fundadora y directora médica de Aurea. Especialista en dermatología estética con más de 12 años de experiencia y formación en Barcelona.',
         'https://images.unsplash.com/photo-1659353888906-adb3e0041693?w=400&h=400&fit=crop&crop=faces&auto=format&q=80'),

        ('patricia.nunez@aureabeautyclinic.com', 'Microneedling', 'EX-10567',
         'Especialista en medicina regenerativa facial. Certificada en microneedling e inducción de colágeno por la Sociedad Europea de Medicina Estética.',
         'https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=400&h=400&fit=crop&crop=faces&auto=format&q=80'),

        ('luis.reyes@aureabeautyclinic.com', 'Masaje relajante corporal', 'EX-10891',
         'Fisioterapeuta y masajista terapéutico. Especializado en drenaje linfático, liberación miofascial y protocolos de relajación profunda.',
         'https://images.unsplash.com/photo-1622253692010-333f2da6031d?w=400&h=400&fit=crop&crop=faces&auto=format&q=80'),

        ('ricardo.fernandez@aureabeautyclinic.com', 'Depilación láser', 'EX-11204',
         'Experto en tecnología láser aplicada a la depilación definitiva. Certificado en equipos de diodo y Nd:YAG para todo tipo de fototipos.',
         'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=400&h=400&fit=crop&crop=faces&auto=format&q=80'),

        ('yamile.santana@aureabeautyclinic.com', 'Peeling químico', 'EX-11530',
         'Dermatóloga especializada en peelings médicos y tratamientos despigmentantes. Enfocada en pieles sensibles y melasma.',
         'https://images.unsplash.com/photo-1643297654416-05795d62e39c?w=400&h=400&fit=crop&crop=faces&auto=format&q=80'),

        ('carolina.pena@aureabeautyclinic.com', 'Limpieza facial profunda', 'EX-11876',
         'Cosmiatra especializada en cuidado facial, extracción e hidratación profunda. Diseña protocolos personalizados según el tipo de piel.',
         'https://images.unsplash.com/photo-1594824476967-48c8b964273f?w=400&h=400&fit=crop&crop=faces&auto=format&q=80'),

        ('manuel.guzman@aureabeautyclinic.com', 'Consulta de valoración', 'EX-12045',
         'Médico estético. Realiza la evaluación inicial y coordina el plan de tratamiento con el resto del equipo de la clínica.',
         'https://images.unsplash.com/photo-1560250097-0b93528c311a?w=400&h=400&fit=crop&crop=faces&auto=format&q=80')
    ) AS v (Email, SpecialtyName, LicenseNumber, Biography, PhotoURL)
    JOIN Users u       ON u.Email = v.Email
    JOIN Specialties s ON s.Name  = v.SpecialtyName
) AS source
ON target.UserId = source.UserId
WHEN MATCHED THEN
    UPDATE SET
        SpecialtyId   = source.SpecialtyId,
        LicenseNumber = source.LicenseNumber,
        Biography     = source.Biography,
        PhotoURL      = source.PhotoURL,
        IsActive      = 1
WHEN NOT MATCHED THEN
    INSERT (UserId, SpecialtyId, LicenseNumber, Biography, PhotoURL, IsActive)
    VALUES (source.UserId, source.SpecialtyId, source.LicenseNumber,
            source.Biography, source.PhotoURL, 1);
GO

------------------------------------------------------------
-- 4. Verificación
------------------------------------------------------------
SELECT SpecialtyId, Name, IsActive FROM Specialties ORDER BY SpecialtyId;

SELECT d.DoctorId,
       u.FirstName + ' ' + u.LastName AS Doctor,
       s.Name                          AS Especialidad,
       d.LicenseNumber,
       CASE WHEN d.PhotoURL IS NULL THEN 'sin foto' ELSE 'con foto' END AS Foto,
       d.IsActive
FROM Doctors d
JOIN Users u       ON u.UserId      = d.UserId
JOIN Specialties s ON s.SpecialtyId = d.SpecialtyId
ORDER BY d.DoctorId;
GO
