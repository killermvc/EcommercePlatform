# Use the official image as a base
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Common/Common.csproj", "Common/"]
COPY ["CartService/CartService.csproj", "CartService/"]
RUN dotnet restore "CartService/CartService.csproj"

COPY Common/ /src/Common
COPY CartService/ /src/CartService
WORKDIR "/src/CartService"
RUN dotnet build "CartService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CartService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartService.dll"]