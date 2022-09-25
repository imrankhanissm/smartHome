# Build
FROM mcr.microsoft.com/dotnet/sdk:5.0.102-1-alpine3.12-amd64 AS build

WORKDIR /build

COPY *.csproj .

RUN dotnet restore

COPY . .

RUN dotnet publish --no-restore -c Release -o out


# Deploy
FROM mcr.microsoft.com/dotnet/sdk:5.0.102-1-alpine3.12-amd64 as final

WORKDIR /app

ENV DBConnectionString server=database;database=smartHome;user=root;password=imran

COPY --from=build /build/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "smartHome.dll"]