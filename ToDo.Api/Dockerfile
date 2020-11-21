#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ToDo.Api/ToDo.Api.csproj", "ToDo.Api/"]
COPY ["ToDo.DataAccess/ToDo.DataAccess.csproj", "ToDo.DataAccess/"]
COPY ["ToDo.Entities/ToDo.Entities.csproj", "ToDo.Entities/"]
COPY ["ToDo.Core/ToDo.Core.csproj", "ToDo.Core/"]
COPY ["ToDo.Business/ToDo.Business.csproj", "ToDo.Business/"]
RUN dotnet restore "ToDo.Api/ToDo.Api.csproj"
COPY . .
WORKDIR "/src/ToDo.Api"
RUN dotnet build "ToDo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo.Api.dll"]