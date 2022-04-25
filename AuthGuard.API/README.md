## Roof Stacks Test Case - Auth Guard API
Based on
- [More about .NET Core](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Swagger Ui](https://swagger.io/tools/swagger-ui/)
- [Fluent Validation](https://fluentvalidation.net/)
- [JIL](https://github.com/kevin-montrose/Jil)
- [xUnit](https://xunit.net/)
- [Moq](https://github.com/moq/moq)

### Run local with CLI
1. Clone or download this repository to local machine.
2. Install [.NET Core SDK for your platform](https://www.microsoft.com/net/core#windowscmd) if didn't install yet.
3. `cd AuthGuard`
4. `dotnet restore`
5. `dotnet run`

### Run on Rider
1. Install [Rider for your platform](https://www.jetbrains.com/rider/) if didn't install yet.
2. Open project
3. Debug => Start debugging
4. You can use swagger ui for your local test => [link](https://localhost:6001/index.html)

### About Project

There is one end point in project.

1. Token : This endpoint generate jwt bearer token for employee api. Example curl sample is below:


    curl --request POST \
    --url https://localhost:6001/api/authentication/token \
    --header 'Content-Type: application/json' \
    --header 'accept: */*' \
    --data '{
    "username": "elgun",
    "password": "qwerty",
    "clientId": "roofstacksclient",
    "clientSecret": "roofstackssecret",
    "clientScopes": [
    "Employee.API"
    ],
    "grantType": "password"
    }'

2. You can follow the versions on AuthGuard.API.csproj (PropertyGroup > Version) file and CHANGELOG.md

3. Test solution structure is designed like a project.