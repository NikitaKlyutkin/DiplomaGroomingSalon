#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DiplomaGroomingSalon/DiplomaGroomingSalon.csproj", "DiplomaGroomingSalon/"]
COPY ["DiplomaGroomingSalon.DAL/DiplomaGroomingSalon.DAL.csproj", "DiplomaGroomingSalon.DAL/"]
COPY ["GroomingSalon.Domain/DiplomaGroomingSalon.Domain.csproj", "GroomingSalon.Domain/"]
COPY ["DiplomaGroomingSalon.Service/DiplomaGroomingSalon.Service.csproj", "DiplomaGroomingSalon.Service/"]
RUN dotnet restore "DiplomaGroomingSalon/DiplomaGroomingSalon.csproj"
COPY . .
WORKDIR "/src/DiplomaGroomingSalon"
RUN dotnet build "DiplomaGroomingSalon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiplomaGroomingSalon.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiplomaGroomingSalon.dll"]