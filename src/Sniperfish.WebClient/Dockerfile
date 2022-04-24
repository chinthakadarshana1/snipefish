#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sniperfish.WebClient/Snipefish.WebClient.csproj", "Sniperfish.WebClient/"]
RUN dotnet restore "Sniperfish.WebClient/Snipefish.WebClient.csproj"
COPY . .
WORKDIR "/src/Sniperfish.WebClient"
RUN dotnet build "Snipefish.WebClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Snipefish.WebClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Snipefish.WebClient.dll"]