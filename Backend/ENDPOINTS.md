# AureaBeautyClinic API — Documentación de Endpoints

Todas las respuestas siguen la misma estructura envolvente:

```json
{
  "success": true,
  "message": "Mensaje descriptivo o null",
  "data": { ... },
  "errors": null
}
```

En caso de error:

```json
{
  "success": false,
  "message": "Descripción del error",
  "data": null,
  "errors": ["detalle del error"]
}
```

---

## Autenticación

Los endpoints marcados con 🔒 requieren el header:
```
Authorization: Bearer <token>
```

---

## 🔐 Auth

### POST `/api/auth/register`
Registra un nuevo usuario y devuelve un token JWT.

**Request body:**
```json
{
  "roleId": 1,
  "name": "Hector",
  "lastName": "Medina",
  "email": "hector@example.com",
  "password": "secreto123",
  "phone": "809-555-0000"
}
```

| Campo      | Tipo     | Requerido | Validación                        |
|------------|----------|-----------|-----------------------------------|
| `roleId`   | `int`    | Sí        | Debe existir en la tabla Roles    |
| `name`     | `string` | Sí        | 2–100 caracteres                  |
| `lastName` | `string` | Sí        | 2–100 caracteres                  |
| `email`    | `string` | Sí        | Formato email válido, único en BD |
| `password` | `string` | Sí        | 8–100 caracteres                  |
| `phone`    | `string` | No        | Formato teléfono válido           |

**Response `200 OK`:**
```json
{
  "success": true,
  "message": "User registered successfully.",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiresAt": "2026-06-04T13:00:00Z",
    "user": {
      "userId": 1,
      "roleId": 1,
      "name": "Hector",
      "lastName": "Medina",
      "fullName": "Hector Medina",
      "email": "hector@example.com",
      "phoneNumber": "809-555-0000",
      "registerDate": "2026-06-04T12:00:00Z",
      "isActive": true,
      "role": {
        "roleId": 1,
        "name": "Patient",
        "description": "Paciente / cliente"
      }
    }
  },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `400`  | Validation failed. (campos inválidos) |
| `409`  | Email 'x@x.com' is already registered. |

---

### POST `/api/auth/login`
Autentica un usuario existente y devuelve un token JWT.

**Request body:**
```json
{
  "email": "hector@example.com",
  "password": "secreto123"
}
```

| Campo      | Tipo     | Requerido |
|------------|----------|-----------|
| `email`    | `string` | Sí        |
| `password` | `string` | Sí        |

**Response `200 OK`:**
```json
{
  "success": true,
  "message": "Login successful.",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiresAt": "2026-06-04T13:00:00Z",
    "user": {
      "userId": 1,
      "roleId": 1,
      "name": "Hector",
      "lastName": "Medina",
      "fullName": "Hector Medina",
      "email": "hector@example.com",
      "phoneNumber": "809-555-0000",
      "registerDate": "2026-06-04T12:00:00Z",
      "isActive": true,
      "role": {
        "roleId": 1,
        "name": "Patient",
        "description": "Paciente / cliente"
      }
    }
  },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `400`  | Validation failed. |
| `401`  | Invalid email or password. |

---

## 👤 Users 🔒

Todos los endpoints de este grupo requieren JWT.

---

### GET `/api/users`
Devuelve la lista de todos los usuarios.

**Query params:** ninguno.

**Response `200 OK`:**
```json
{
  "success": true,
  "message": null,
  "data": [
    {
      "userId": 1,
      "roleId": 1,
      "name": "Hector",
      "lastName": "Medina",
      "fullName": "Hector Medina",
      "email": "hector@example.com",
      "phoneNumber": "809-555-0000",
      "registerDate": "2026-06-04T12:00:00Z",
      "isActive": true,
      "role": {
        "roleId": 1,
        "name": "Patient",
        "description": "Paciente / cliente"
      }
    }
  ],
  "errors": null
}
```

---

### GET `/api/users/{id}`
Devuelve un usuario por ID.

**Path param:** `id` — ID del usuario (`int`).

**Response `200 OK`:**
```json
{
  "success": true,
  "message": null,
  "data": {
    "userId": 1,
    "roleId": 1,
    "name": "Hector",
    "lastName": "Medina",
    "fullName": "Hector Medina",
    "email": "hector@example.com",
    "phoneNumber": "809-555-0000",
    "registerDate": "2026-06-04T12:00:00Z",
    "isActive": true,
    "role": {
      "roleId": 1,
      "name": "Patient",
      "description": "Paciente / cliente"
    }
  },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | User with ID {id} was not found. |

---

### PUT `/api/users/{id}` 🔒
Actualiza los datos de un usuario. Todos los campos son opcionales; solo se aplican los que se envíen.

**Path param:** `id` — ID del usuario (`int`).

**Request body:**
```json
{
  "name": "Hector",
  "lastName": "Medina",
  "phone": "809-555-9999",
  "isActive": true
}
```

| Campo      | Tipo      | Requerido | Validación          |
|------------|-----------|-----------|---------------------|
| `name`     | `string`  | No        | 2–100 caracteres    |
| `lastName` | `string`  | No        | 2–100 caracteres    |
| `phone`    | `string`  | No        | Formato teléfono    |
| `isActive` | `boolean` | No        | —                   |

**Response `200 OK`:**
```json
{
  "success": true,
  "message": "User updated successfully.",
  "data": {
    "userId": 1,
    "roleId": 1,
    "name": "Hector",
    "lastName": "Medina",
    "fullName": "Hector Medina",
    "email": "hector@example.com",
    "phoneNumber": "809-555-9999",
    "registerDate": "2026-06-04T12:00:00Z",
    "isActive": true,
    "role": { "roleId": 1, "name": "Patient", "description": "Paciente / cliente" }
  },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | User with ID {id} was not found. |

---

## 🎭 Roles

### GET `/api/roles`
Devuelve la lista de todos los roles.

**Query params:** ninguno.

**Response `200 OK`:**
```json
{
  "success": true,
  "message": null,
  "data": [
    { "roleId": 1, "name": "Admin",   "description": "Administrador del sistema" },
    { "roleId": 2, "name": "Doctor",  "description": "Médico de la clínica" },
    { "roleId": 3, "name": "Patient", "description": "Paciente / cliente" }
  ],
  "errors": null
}
```

---

### GET `/api/roles/{id}`
Devuelve un rol por ID.

**Path param:** `id` — ID del rol (`int`).

**Response `200 OK`:**
```json
{
  "success": true,
  "message": null,
  "data": { "roleId": 1, "name": "Admin", "description": "Administrador del sistema" },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | Role with ID {id} was not found. |

---

### POST `/api/roles`
Crea un nuevo rol.

**Request body:**
```json
{
  "name": "Receptionist",
  "description": "Recepcionista de la clínica"
}
```

| Campo         | Tipo     | Requerido | Validación       |
|---------------|----------|-----------|------------------|
| `name`        | `string` | Sí        | 2–100 caracteres |
| `description` | `string` | No        | Máx 500 chars    |

**Response `201 Created`:**
```json
{
  "success": true,
  "message": "Role created successfully.",
  "data": { "roleId": 4, "name": "Receptionist", "description": "Recepcionista de la clínica" },
  "errors": null
}
```

---

### PUT `/api/roles/{id}`
Actualiza un rol existente.

**Path param:** `id` — ID del rol (`int`).

**Request body:**
```json
{
  "name": "Receptionist",
  "description": "Descripción actualizada"
}
```

| Campo         | Tipo     | Requerido | Validación       |
|---------------|----------|-----------|------------------|
| `name`        | `string` | Sí        | 2–100 caracteres |
| `description` | `string` | No        | Máx 500 chars    |

**Response `200 OK`:**
```json
{
  "success": true,
  "message": "Role updated successfully.",
  "data": { "roleId": 4, "name": "Receptionist", "description": "Descripción actualizada" },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | Role with ID {id} was not found. |

---

## 🏥 Specialties

### GET `/api/specialties`
Devuelve la lista de todas las especialidades.

**Response `200 OK`:**
```json
{
  "success": true,
  "message": null,
  "data": [
    { "specialtyId": 1, "name": "Dermatología", "description": "Piel y cabello", "isActive": true }
  ],
  "errors": null
}
```

---

### GET `/api/specialties/{id}`
Devuelve una especialidad por ID.

**Path param:** `id` — ID de la especialidad (`int`).

**Response `200 OK`:**
```json
{
  "success": true,
  "message": null,
  "data": { "specialtyId": 1, "name": "Dermatología", "description": "Piel y cabello", "isActive": true },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | Specialty with ID {id} was not found. |

---

### POST `/api/specialties`
Crea una nueva especialidad.

**Request body:**
```json
{
  "name": "Dermatología",
  "description": "Piel y cabello",
  "isActive": true
}
```

| Campo         | Tipo      | Requerido | Validación       |
|---------------|-----------|-----------|------------------|
| `name`        | `string`  | Sí        | 2–100 caracteres |
| `description` | `string`  | No        | Máx 500 chars    |
| `isActive`    | `boolean` | No        | Default: `true`  |

**Response `201 Created`:**
```json
{
  "success": true,
  "message": "Specialty created successfully.",
  "data": { "specialtyId": 1, "name": "Dermatología", "description": "Piel y cabello", "isActive": true },
  "errors": null
}
```

---

### PUT `/api/specialties/{id}`
Actualiza una especialidad existente.

**Path param:** `id` — ID de la especialidad (`int`).

**Request body:**
```json
{
  "name": "Dermatología Clínica",
  "description": "Descripción actualizada",
  "isActive": false
}
```

| Campo         | Tipo      | Requerido | Validación       |
|---------------|-----------|-----------|------------------|
| `name`        | `string`  | Sí        | 2–100 caracteres |
| `description` | `string`  | No        | Máx 500 chars    |
| `isActive`    | `boolean` | Sí        | —                |

**Response `200 OK`:**
```json
{
  "success": true,
  "message": "Specialty updated successfully.",
  "data": { "specialtyId": 1, "name": "Dermatología Clínica", "description": "Descripción actualizada", "isActive": false },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | Specialty with ID {id} was not found. |

---

## 👨‍⚕️ Doctors

### GET `/api/doctors`
Devuelve la lista de todos los doctores con sus datos de usuario y especialidad.

**Response `200 OK`:**
```json
{
  "success": true,
  "message": null,
  "data": [
    {
      "doctorId": 1,
      "userId": 2,
      "specialtyId": 1,
      "licenseNumber": "LIC-001",
      "biography": "Especialista con 10 años de experiencia.",
      "photoURL": "https://example.com/foto.jpg",
      "isActive": true,
      "user": {
        "userId": 2, "roleId": 2, "name": "Ana", "lastName": "López",
        "fullName": "Ana López", "email": "ana@example.com",
        "phoneNumber": null, "registerDate": "2026-01-01T00:00:00Z",
        "isActive": true,
        "role": { "roleId": 2, "name": "Doctor", "description": "Médico de la clínica" }
      },
      "specialty": { "specialtyId": 1, "name": "Dermatología", "description": "Piel y cabello", "isActive": true }
    }
  ],
  "errors": null
}
```

---

### GET `/api/doctors/{id}`
Devuelve un doctor por ID.

**Path param:** `id` — ID del doctor (`int`).

**Response `200 OK`:** igual que el objeto individual del array de `GET /api/doctors`.

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | Doctor with ID {id} was not found. |

---

### POST `/api/doctors`
Crea un nuevo doctor. El usuario al que apunta `userId` debe existir previamente.

**Request body:**
```json
{
  "userId": 2,
  "specialtyId": 1,
  "licenseNumber": "LIC-001",
  "biography": "Especialista con 10 años de experiencia.",
  "photoURL": "https://example.com/foto.jpg",
  "isActive": true
}
```

| Campo           | Tipo      | Requerido | Validación           |
|-----------------|-----------|-----------|----------------------|
| `userId`        | `int`     | Sí        | Debe existir en BD   |
| `specialtyId`   | `int`     | Sí        | Debe existir en BD   |
| `licenseNumber` | `string`  | No        | Máx 50 chars         |
| `biography`     | `string`  | No        | Máx 1000 chars       |
| `photoURL`      | `string`  | No        | URL válida           |
| `isActive`      | `boolean` | No        | Default: `true`      |

**Response `201 Created`:** objeto `DoctorDTO` completo (igual a GET por ID).

---

### PUT `/api/doctors/{id}`
Actualiza un doctor existente.

**Path param:** `id` — ID del doctor (`int`).

**Request body:**
```json
{
  "specialtyId": 2,
  "licenseNumber": "LIC-002",
  "biography": "Bio actualizada.",
  "photoURL": "https://example.com/nueva-foto.jpg",
  "isActive": true
}
```

| Campo           | Tipo      | Requerido | Validación     |
|-----------------|-----------|-----------|----------------|
| `specialtyId`   | `int`     | Sí        | —              |
| `licenseNumber` | `string`  | No        | Máx 50 chars   |
| `biography`     | `string`  | No        | Máx 1000 chars |
| `photoURL`      | `string`  | No        | URL válida     |
| `isActive`      | `boolean` | Sí        | —              |

**Response `200 OK`:**
```json
{
  "success": true,
  "message": "Doctor updated successfully.",
  "data": { "...objeto DoctorDTO actualizado..." },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | Doctor with ID {id} was not found. |

---

## 📅 Appointments

### GET `/api/appointment`
Devuelve todas las citas con datos de usuario y doctor.

**Response `200 OK`:**
```json
{
  "success": true,
  "message": null,
  "data": [
    {
      "appointmentId": 1,
      "userId": 1,
      "doctorId": 1,
      "scheduled": "2026-06-10T09:00:00Z",
      "state": "Pending",
      "notes": "Primera consulta.",
      "created": "2026-06-04T12:00:00Z",
      "user": { "...UserDTO..." },
      "doctor": { "...DoctorDTO..." }
    }
  ],
  "errors": null
}
```

---

### GET `/api/appointment/{id}`
Devuelve una cita por ID.

**Path param:** `id` — ID de la cita (`int`).

**Response `200 OK`:** objeto `AppointmentDTO` individual.

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | Appointment with ID {id} was not found. |

---

### GET `/api/appointment/user/{userId}`
Devuelve todas las citas de un usuario específico.

**Path param:** `userId` — ID del usuario (`int`).

**Response `200 OK`:** array de `AppointmentDTO`.

---

### GET `/api/appointment/doctor/{doctorId}`
Devuelve todas las citas de un doctor específico.

**Path param:** `doctorId` — ID del doctor (`int`).

**Response `200 OK`:** array de `AppointmentDTO`.

---

### POST `/api/appointment`
Crea una nueva cita. El estado inicial siempre es `Pending`.

**Request body:**
```json
{
  "userId": 1,
  "doctorId": 1,
  "scheduled": "2026-06-10T09:00:00Z",
  "notes": "Primera consulta."
}
```

| Campo       | Tipo       | Requerido | Validación                   |
|-------------|------------|-----------|------------------------------|
| `userId`    | `int`      | Sí        | Debe existir en BD           |
| `doctorId`  | `int`      | Sí        | Debe existir en BD           |
| `scheduled` | `datetime` | Sí        | Formato ISO 8601             |
| `notes`     | `string`   | No        | Máx 500 chars                |

**Response `201 Created`:** objeto `AppointmentDTO` completo con `state: "Pending"`.

---

### PUT `/api/appointment/{id}`
Actualiza el estado, fecha o notas de una cita.

**Path param:** `id` — ID de la cita (`int`).

**Request body:**
```json
{
  "scheduled": "2026-06-11T10:00:00Z",
  "state": "Confirmed",
  "notes": "Confirmada por el doctor."
}
```

| Campo       | Tipo       | Requerido | Validación                                               |
|-------------|------------|-----------|----------------------------------------------------------|
| `scheduled` | `datetime` | Sí        | Formato ISO 8601                                         |
| `state`     | `string`   | Sí        | Uno de: `Pending`, `Confirmed`, `Cancelled`, `Completed` |
| `notes`     | `string`   | No        | Máx 500 chars                                            |

**Response `200 OK`:**
```json
{
  "success": true,
  "message": "Appointment updated successfully.",
  "data": { "...AppointmentDTO actualizado..." },
  "errors": null
}
```

**Errores posibles:**

| Código | Mensaje |
|--------|---------|
| `404`  | Appointment with ID {id} was not found. |
| `400`  | Invalid state. Valid values are: Pending, Confirmed, Cancelled, Completed. |
