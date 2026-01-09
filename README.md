# RestaurantBackend – Notas de entrega

## Tecnologías y versiones

* .NET: 8 (net8.0)
* C#: 12
* ASP.NET Core Web API
* ORM: Entity Framework Core 8
* Proveedor EF Core: Oracle.EntityFrameworkCore (Oracle)
* Base de datos: Oracle XE 21c (imagen `gvenzl/oracle-xe:21-slim`)
* Docker / Docker Compose: para levantar la BD en local

## Estructura del proyecto (n-capas)

* `RestaurantBackend.Api`: capa de presentación (controllers, middleware, configuración DI)
* `RestaurantBackend.Application`: capa de aplicación (DTOs, interfaces, servicios)
* `RestaurantBackend.Infrastructure`: capa infraestructura/persistencia (EF Core DbContext, entidades, repositorios, persistences)

## Base de datos (Oracle)

### Scripts

Los scripts de creación y seed están en:

* `db/init/01-schema.sql` (estructura)
* `db/init/02-seed.sql` (seed)

> Nota: Los scripts se ejecutan dentro de la PDB `XEPDB1` y crean los objetos en el schema `RESTAURANTE`.

### Levantar Oracle con Docker (local)

Archivo:

* `docker-compose.db.yml`

Comando:

    docker compose -f docker-compose.db.yml up -d

Puertos:

* Oracle: `localhost:1522` -> contenedor `1521`

Credenciales:

* Usuario: `restaurante`
* Password: `restaurant`
* Service Name: `XEPDB1`

Conexión en DBeaver:

* Host: `localhost`
* Port: `1522`
* Service name: `XEPDB1`
* User: `restaurante`
* Password: `restaurant`

Reset de BD (re-ejecuta scripts init):

    docker compose -f docker-compose.db.yml down -v docker compose -f docker-compose.db.yml up -d

## Configuración del backend (local)

Cadena de conexión (local) en:

* `RestaurantBackend.Api/appsettings.Development.json`

Ejemplo:

* `User Id=restaurante;Password=restaurant;Data Source=localhost:1522/XEPDB1;`

## Ejecutar el backend (Visual Studio)

1. Abrir la solución en Visual Studio 2022.
2. Establecer `RestaurantBackend.Api` como proyecto de inicio.
3. Ejecutar con __Iniciar depuración__ (F5) o __Iniciar sin depurar__ (Ctrl+F5).
4. Swagger (si aplica): `https://localhost:<puerto>/swagger`

## Publicación desde Visual Studio

1. Click derecho en `RestaurantBackend.Api` → __Publicar...__
2. Target: **Folder**
3. Configuración: `Release`
4. Framework: `net8.0`
5. Publicar

El resultado genera la carpeta de publicación (artefacto) que se incluye en el `.zip`.

## Despliegue en VPS (referencia)

* Backend publicado ejecutándose en el host (fuera de Docker).
* Oracle desplegado con Docker en la VPS.

Cadena de conexión en VPS:

* Si Oracle expone `1522` en la VPS:`User Id=restaurante;Password=restaurant;Data Source=localhost:1522/XEPDB1;`

Recomendación: configurar la cadena por variable de entorno:

* `ConnectionStrings__DefaultConnection=User Id=restaurante;Password=restaurant;Data Source=localhost:1522/XEPDB1;`

## Entregables incluidos

* Script de base de datos Oracle (`db/init/01-schema.sql` y `db/init/02-seed.sql`)
* Código fuente compilable
* Publicación generada desde Visual Studio (carpeta publish)
* Archivo(s) Docker Compose para levantar la BD
