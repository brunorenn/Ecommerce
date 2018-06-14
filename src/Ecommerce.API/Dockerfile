FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 58858
EXPOSE 44356

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
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
