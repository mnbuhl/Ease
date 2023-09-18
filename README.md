# Eaze

Eaze is a project starting point for building web applications using React or Vue, but without the hassle of 
frontend state management thanks to Inertia.js.

The template is pre-configured with ASP.NET Core Identity, Entity Framework Core, 
and an opinionated architecture to get you up and running quickly.

The frontend is built with Tailwind CSS and Vite, and is ready to be extended with your own components.

The backend template has all configuration directly implemented in the template, so you don't need to perform gymnastics
to configure your project.

Eaze is heavily inspired and borrows (<sup><sub>literally steals...</sub></sup>) a lot
from [Laravel Breeze](https://github.com/laravel/breeze).

## TODO

- [ ] : Finish backend implementations
- [ ] : Convert to template
- [ ] : Add SSR support
- [ ] : Publish to NuGet
- [ ] : Add Vue support
- [ ] : Add tests
- [ ] : Add documentation
- [ ] : Add Docker support
- [ ] : Add CI/CD
- [ ] : Use tailwind theming for easier customization
- [ ] : Configure default email layout

### Getting Started

Install the template from NuGet:

```bash
dotnet new -i Eaze.Templates
```

Create a new project:

```bash
dotnet new eaze-react -n MyProject
dotnet new eaze-vue -n MyProject
```

### Features

- ASP.NET Core 8.0
- ASP.NET Core Identity
- Entity Framework Core
- SSR or CSR
- Inertia.js
- React or Vue
- Tailwind CSS
- Vite
- TypeScript

### Disclaimer

This template is still in development and is not ready for production use.

If anyone from the Laravel team has a problem with me using their frontend code, please let me know and I will change it.