FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/WebAPI/EduTrack.WebAPI.csproj", "src/WebAPI/"]
COPY ["src/Application/EduTrack.Application.csproj", "src/Application/"]
COPY ["src/Domain/EduTrack.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/EduTrack.Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/WebAPI/EduTrack.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/WebAPI"
RUN dotnet build "EduTrack.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EduTrack.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EduTrack.WebAPI.dll"]