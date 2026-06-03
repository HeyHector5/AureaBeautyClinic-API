/*
    AureaBeautyClinic - Database schema
    Generado a partir de las entidades en AureaBeautyClinic.Shared.Entities

    Orden de creación (por dependencias de FK):
        1. Roles
        2. Specialties
        3. Users        (FK -> Roles)
        4. Doctors      (FK -> Users, Specialties)
        5. Appointments (FK -> Users, Doctors)

    Los nombres de las constraints (PK/FK/UQ) se dejan sin especificar
    para que SQL Server los genere automáticamente.
*/

CREATE TABLE Roles
(
    RoleId      INT             IDENTITY(1,1) PRIMARY KEY,
    Name        NVARCHAR(100)   NOT NULL,
    Description  NVARCHAR(500)   NULL
);
GO

CREATE TABLE Specialties
(
    SpecialtyId  INT             IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(100)   NOT NULL,
    Description  NVARCHAR(500)   NULL,
    IsActive     BIT             NOT NULL
);
GO

CREATE TABLE Users
(
    UserId       INT             IDENTITY(1,1) PRIMARY KEY,
    RoleId       INT             NOT NULL FOREIGN KEY REFERENCES Roles(RoleId),
    FirstName    NVARCHAR(100)   NOT NULL,
    LastName     NVARCHAR(100)   NOT NULL,
    Email        NVARCHAR(256)   NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255)   NOT NULL,
    Phone        NVARCHAR(30)    NULL,
    Registered   DATETIME2       NOT NULL,
    IsActive     BIT             NOT NULL
);
GO

CREATE TABLE Doctors
(
    DoctorId      INT             IDENTITY(1,1) PRIMARY KEY,
    UserId        INT             NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    SpecialtyId   INT             NOT NULL FOREIGN KEY REFERENCES Specialties(SpecialtyId),
    LicenseNumber NVARCHAR(50)    NULL,
    Biography     NVARCHAR(1000)  NULL,
    PhotoURL      NVARCHAR(500)   NULL,
    IsActive      BIT             NOT NULL
);
GO

CREATE TABLE Appointments
(
    AppointmentId INT             IDENTITY(1,1) PRIMARY KEY,
    UserId        INT             NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    DoctorId      INT             NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorId),
    Scheduled     DATETIME2       NOT NULL,
    State         NVARCHAR(50)    NOT NULL,
    Notes         NVARCHAR(500)   NULL,
    Created       DATETIME2       NOT NULL
);
GO
