# Ease

Ease is a project starting point for building web applications using React or Vue, but without the hassle of
frontend state management thanks to [Inertia.js](https://inertiajs.com/).

The template is pre-configured with ASP.NET Core Identity, Entity Framework Core,
and Inertia to get you up and running quickly.

The frontend is built with Tailwind CSS and Vite, and is ready to be extended with your own components.

The backend template has all configuration directly implemented in the template, so you don't need to perform gymnastics
to configure your project.

Ease is heavily inspired and borrows (<sub><sup><sub>literally steals...</sub></sup></sub>) a lot
from [Laravel Breeze](https://github.com/laravel/breeze).

## TODO

- [x] : Finish backend implementations
- [x] : Convert to template
- [x] : Publish to NuGet
- [x] : Add SSR support
- [ ] : Simplify setup process
- [ ] : Add CI/CD
- [ ] : Add landing page
- [ ] : Add Vue support
- [ ] : Add documentation
- [ ] : Add tests
- [ ] : Add Docker support
- [ ] : Use tailwind theming for easier customization
- [ ] : Configure default email layout
- [ ] : JavaScript variants?

### Getting Started

Install the templates from NuGet:

```bash
dotnet new -i Ease.Templates
```

### Create a new project:

React:
```bash
dotnet new ease -n {MyProject} --client react
```

Vue (coming soon):

```bash
dotnet new ease -n {MyProject} --client vue
```

Headless (configure your own frontend)

```bash
dotnet new ease -n {MyProject} --client headless
```

The default database is PostgreSQL, but you can change it to SQL Server or SQLite by adding the `--database`
option:

```bash
dotnet new ease -n {MyProject} --client react --database sqlite || sqlserver || postgresql
```

*** MySQL is currently not supported because Pomelo has not updated their adapter to .NET 8.0

### Run the project

```bash
dotnet run --seed && dotnet run
```

```bash
cd client
npm i && npm run dev
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