# SLHGraphQLExample
Graphql with .NET Core 3.1

```
dotnet add package HotChocolate.AspNetCore

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

app.MapGraphQL();

```

```sql
CREATE TABLE [dbo].[Tbl_Fruit](
	[Fruit_Id] [int] IDENTITY(1,1) NOT NULL,
	[Fruit_Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Fruit] PRIMARY KEY CLUSTERED 
(
	[Fruit_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Tbl_Blog](
	[Blog_Id] [int] IDENTITY(1,1) NOT NULL,
	[Blog_Title] [nvarchar](50) NULL,
	[Blog_Author] [nvarchar](50) NULL,
	[Blog_Content] [nvarchar](200) NULL,
 CONSTRAINT [PK_Tbl_Blog] PRIMARY KEY CLUSTERED 
(
	[Blog_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```
