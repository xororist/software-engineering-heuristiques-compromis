# syntax=docker/dockerfile:1

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

# Copy all source code
COPY . .

# Navigate to the Web API project and build
WORKDIR /source/ParkingReservation/ParkingReservation.WebApi

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore && \
    dotnet publish --configuration Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

COPY --from=build /app .

USER $APP_UID

ENTRYPOINT ["dotnet", "ParkingReservation.WebApi.dll"]