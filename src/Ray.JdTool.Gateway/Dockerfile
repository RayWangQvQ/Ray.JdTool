#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 9002

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Ray.JdTool.Gateway/Ray.JdTool.Gateway.csproj", "src/Ray.JdTool.Gateway/"]
RUN dotnet restore "src/Ray.JdTool.Gateway/Ray.JdTool.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Ray.JdTool.Gateway"
RUN dotnet build "Ray.JdTool.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ray.JdTool.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ray.JdTool.Gateway.dll"]