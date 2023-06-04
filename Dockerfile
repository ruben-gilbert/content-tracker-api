# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-stage
ENV ASPNETCORE_ENVIRONMENT=Development

COPY ./ContentTracker /ContentTracker

WORKDIR /ContentTracker
RUN dotnet restore
RUN dotnet publish -o build

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /ContentTracker
COPY --from=build-stage /ContentTracker/build .