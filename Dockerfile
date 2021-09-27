#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .

COPY ["StorBookWebApp/StorBookWebApp.csproj", "StorBookWebApp/"]
COPY ["StorBookWebApp.Data/StorBookWebApp.Data.csproj", "StorBookWebApp.Data/"]
COPY ["StorBookWebApp.Core/StorBookWebApp.Core.csproj", "StorBookWebApp.Core/"]
COPY ["StorBookWebApp.DTOs/StorBookWebApp.DTOs.csproj", "StorBookWebApp.DTOs/"]
COPY ["StorBookWebApp.Models/StorBookWebApp.Models.csproj", "StorBookWebApp.Models/"]
COPY ["StorBookWebApp.Shared/StorBookWebApp.Shared.csproj", "StorBookWebApp.Shared/"]
RUN dotnet restore "StorBookWebApp/StorBookWebApp.csproj"

COPY . .

WORKDIR /src/StorBookWebApp
RUN dotnet build

FROM build AS publish
WORKDIR /src/StorBookWebApp
RUN dotnet publish -c Release -o /src/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /src/publish .

COPY --from=publish /src/StorBookWebApp/wwwroot/Data/Categories.json Data/
COPY --from=publish /src/StorBookWebApp/wwwroot/Data/Books.json Data/
COPY --from=publish /src/StorBookWebApp/wwwroot/Data/Authors.json Data/
COPY --from=publish /src/StorBookWebApp/wwwroot/Data/BookGenres.json Data/
COPY --from=publish /src/StorBookWebApp/wwwroot/Data/BookAuthors.json Data/
COPY --from=publish /src/StorBookWebApp/wwwroot/Data/Genres.json Data/

#ENTRYPOINT ["dotnet", "StorBookWebApp.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet StorBookWebApp.dll
