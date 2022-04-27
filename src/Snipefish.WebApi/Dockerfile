#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Snipefish.WebApi/Snipefish.WebApi.csproj", "Snipefish.WebApi/"]
COPY ["Snipefish.Domain/Snipefish.Domain.csproj", "Snipefish.Domain/"]
COPY ["Snipefish.Persistence.Mongo/Snipefish.Persistence.Mongo.csproj", "Snipefish.Persistence.Mongo/"]
COPY ["Snipefish.Application/Snipefish.Application.csproj", "Snipefish.Application/"]
RUN dotnet restore "Snipefish.WebApi/Snipefish.WebApi.csproj"
COPY . .
WORKDIR "/src/Snipefish.WebApi"
RUN dotnet build "Snipefish.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Snipefish.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Snipefish.WebApi.dll"]