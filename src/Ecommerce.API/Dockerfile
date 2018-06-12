FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY src/Ecommerce.API/Ecommerce.API.csproj src/Ecommerce.API/
RUN dotnet restore src/Ecommerce.API/Ecommerce.API.csproj
COPY . .
WORKDIR /src/src/Ecommerce.API
RUN dotnet build Ecommerce.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Ecommerce.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ecommerce.API.dll"]
