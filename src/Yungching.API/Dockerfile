# -------------------
# 1. Build Stage
# -------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and restore
COPY yungching.sln yungching.sln
COPY src/Yungching.API/Yungching.API.csproj src/Yungching.API/Yungching.API.csproj
COPY src/Yungching.Application/Yungching.Application.csproj src/Yungching.Application/Yungching.Application.csproj
COPY src/Yungching.Infrastructure/Yungching.Infrastructure.csproj src/Yungching.Infrastructure/Yungching.Infrastructure.csproj
COPY src/Yungching.Domain/Yungching.Domain.csproj src/Yungching.Domain/Yungching.Domain.csproj
COPY test/Yungching.Application.Tests/Yungching.Application.Tests.csproj test/Yungching.Application.Tests/Yungching.Application.Tests.csproj

RUN dotnet restore yungching.sln

# Build app
COPY src/Yungching.API src/Yungching.API
COPY src/Yungching.Application src/Yungching.Application
COPY src/Yungching.Infrastructure src/Yungching.Infrastructure
COPY src/Yungching.Domain src/Yungching.Domain

WORKDIR /app/src/Yungching.API
RUN dotnet publish -c Release -o /app/src/out

# -------------------
# 2. Runtime Stage
# -------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/out ./

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Yungching.API.dll"]
