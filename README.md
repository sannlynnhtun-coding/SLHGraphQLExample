# SLHGraphQLExample
Graphql with .NET Core 3.1

```
dotnet add package HotChocolate.AspNetCore

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

app.MapGraphQL();

```
