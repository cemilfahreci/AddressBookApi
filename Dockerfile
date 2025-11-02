FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://0.0.0.0:10000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AddressBookApi/AddressBookApi.csproj", "AddressBookApi/"]
RUN dotnet restore "AddressBookApi/AddressBookApi.csproj"
COPY . .
WORKDIR "/src/AddressBookApi"
RUN dotnet build "AddressBookApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AddressBookApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AddressBookApi.dll"]

