#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /src
COPY ["SecretSantaService.Web/SecretSantaService.Web.csproj", "SecretSantaService.Web/"]
COPY ["SecretSantaService.Application/SecretSantaService.Application.csproj", "SecretSantaService.Application/"]
COPY ["SecretSantaService.Domain/SecretSantaService.Domain.csproj", "SecretSantaService.Domain/"]
COPY ["SecretSantaService.Infrastructure/SecretSantaService.Infrastructure.csproj", "SecretSantaService.Infrastructure/"]
RUN dotnet restore "SecretSantaService.Web/SecretSantaService.Web.csproj"
COPY . .
WORKDIR "/src/SecretSantaService.Web"
RUN dotnet build "SecretSantaService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecretSantaService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretSantaService.Web.dll"]
