# Aurea Beauty Clinic — Frontend

SPA en **Vue 3** (Composition API, `<script setup>`) con **Vite**, **Tailwind CSS v4**,
**vue-router** y **SweetAlert2**. Consume la API .NET del directorio `Backend/`.

## Requisitos

- Node.js **20.19+** o **22.12+** (Vite 8 no arranca con versiones menores).

## Puesta en marcha

```bash
npm install
npm run dev      # servidor de desarrollo en http://localhost:5173
npm run build    # build de producción en dist/
npm run preview  # sirve el build de producción
```

## Configuración de la API

La URL del backend se lee de `VITE_API_URL`:

| Archivo | Valor | Cuándo se usa |
|---|---|---|
| `.env.development` | `https://localhost:7008` | `npm run dev` |
| `.env.production` | URL de Azure | `npm run build` |

El backend tiene `UseHttpsRedirection()`, así que en local conviene apuntar al puerto
**https** (7008) y confiar el certificado de desarrollo:

```bash
dotnet dev-certs https --trust
```

CORS ya permite `http://localhost:5173` (ver `Cors:AllowedOrigins` en `appsettings.json`).

## Estructura

```
src/
├── components/          Componentes de página pública + AppointmentCard
│   └── ui/              Primitivas reutilizables (BaseButton, DataTable, BaseModal, ...)
├── composables/         useAuth (sesión), useCrud (módulos admin), useMyAppointments
├── constants/           Datos de la clínica y estados de cita
├── layouts/             PublicLayout, ClientLayout, AdminLayout
├── router/              Rutas + guard de autenticación y rol
├── services/            http.js (fetch + Bearer + ApiResponse) y un servicio por recurso
├── utils/               format.js (fechas) y notify.js (SweetAlert2)
└── views/
    ├── admin/           Panel: resumen, citas, usuarios, doctores, especialidades, roles
    └── client/          Cuenta: resumen, mis reservas, historial, perfil
```

## Rutas

| Ruta | Acceso |
|---|---|
| `/`, `/servicios`, `/nosotros`, `/contactos` | pública |
| `/login`, `/register` | pública (redirige si ya hay sesión) |
| `/reservar` | requiere sesión |
| `/dashboard`, `/mis-reservas`, `/historial`, `/perfil` | requiere sesión |
| `/admin/*` | requiere rol `Admin` |
| cualquier otra | página 404 |

## Sistema de diseño

Los tokens de marca se declaran con `@theme` en `src/style.css` (Tailwind v4 ya no
autodescubre `tailwind.config.js`, por eso ese archivo se eliminó):

| Token | Valor | Utilidades |
|---|---|---|
| `--color-aurea` | `#FF3B30` | `text-aurea`, `bg-aurea`, `border-aurea`, `ring-aurea` |
| `--color-aurea-dark` | `#e0342a` | `hover:bg-aurea-dark` |
| `--color-aurea-tint` | `#fff1f0` | `bg-aurea-tint` |
| `--font-display` | Playfair Display | `font-display` |
| `--font-body` | Poppins | `font-body` |

Las tipografías se cargan desde Google Fonts en `index.html`.

## Notas sobre la API

- Todas las respuestas vienen envueltas en `{ success, message, data, errors }`;
  `services/http.js` las desempaqueta y lanza un `ApiError` cuando fallan.
- El prefijo de citas es **singular**: `/api/appointment` (el resto son plurales).
- `DELETE` nunca borra: desactiva el registro (o cancela la cita).
- Ningún endpoint de lista pagina, así que `DataTable` pagina en cliente.
