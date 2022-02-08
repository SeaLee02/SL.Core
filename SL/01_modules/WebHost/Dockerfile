#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Nuget.Config", "."]
COPY ["01_modules/WebHost/WebHost.csproj", "01_modules/WebHost/"]
RUN dotnet restore "01_modules/WebHost/WebHost.csproj"
COPY . .
WORKDIR "/src/01_modules/WebHost"
RUN dotnet build "WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebHost.dll"]