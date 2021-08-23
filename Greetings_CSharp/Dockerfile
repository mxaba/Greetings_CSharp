FROM mcr.microsoft.com/dotnet/core/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distict layers
COPY Greetings_CSharp.csproj ./

RUN dotnet restore

# Copy everything else and build
COPY . .

# Publish
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Greetings_CSharp.dll" ]