FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env

WORKDIR /app

COPY Greetings_CSharp.csproj ./

# Copy csproj and restore as distict layers
RUN dotnet restore

# Copy everything else and build
COPY . .

# Publish
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim

WORKDIR /app

COPY --from=build-env /app/out .

# We will have to supply ASPNETCORE_URLS variable for it to be run on Heroku.
CMD ASPNETCORE\_URLS=http://\*:$PORT dotnet Greetings_CSharp.dll
