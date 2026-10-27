FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
RUN mkdir -p /data && chown -R app:app /data
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DesafioPicPay/DesafioPicPay.csproj", "DesafioPicPay/"]
RUN dotnet restore "DesafioPicPay/DesafioPicPay.csproj"
COPY . .
WORKDIR "/src/DesafioPicPay"
RUN dotnet build "./DesafioPicPay.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DesafioPicPay.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioPicPay.dll"]
