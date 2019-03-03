# identityserver4-dockersample-dotnetcore-nginx

A starter project for the following tech stack:

- [IdentityServer4](http://docs.identityserver.io/en/latest/)
- ASP.NET Core
- NGINX
- HTTPS
- Docker

## Get Started

```
docker-compose up
```

You can make http requests. For this, I use [postman](https://www.getpostman.com/):

Get the openid configuration:

```
GET https://localhost/authentication/.well-known/openid-configuration
```

Get an access token:

```
POST https://localhost/authentication/connect/token
--- Request Body ---
client_id=SampleApp.Mobile
client_secret=secret
username=ben
password=password
scope=openid SampleApp.API
grant_type=password
```

Response:

```json
{
  "access_token": "token",
  "expires_in": 2592000,
  "token_type": "Bearer"
}
```

You can now make requests against the API:

```
GET https://localhost/api/identity
--- Headers ---
Authorization: Bearer token
```

If you don't add the `Authorization` header, you will get a 401.

## Debugging the app

Docker is set up to only run the app. To debug this, you will need to change the following lines:

In SampleApp.Authentication/Startup.cs, comment out the line like below:

```csharp
// options.PublicOrigin = "http://localhost:4000";
```

In SampleApp.API/Startup.cs, replace the `Authority`:

```csharp
AddIdentityServerAuthentication(options =>
{
    options.Authority = "http://localhost:4000/authentication";
    options.RequireHttpsMetadata = false;
    options.ApiName = "SampleApp.API";
});
```

In a real project, you would set these values in a configuration file, so you might have something like:

- `appsettings.json` - development configurations
- `appsettings.Staging.json` - docker development configuration
- `appsettings.Production.json` - docker production configurations

You would then want to reproduce this pattern for `nginx.conf`, `docker-compose` and `Dockerfile`.
