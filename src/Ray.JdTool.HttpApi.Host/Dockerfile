#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 9000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Ray.JdTool.HttpApi.Host/Ray.JdTool.HttpApi.Host.csproj", "src/Ray.JdTool.HttpApi.Host/"]
COPY ["src/Ray.JdTool.Application/Ray.JdTool.Application.csproj", "src/Ray.JdTool.Application/"]
COPY ["src/Ray.JdTool.Application.Contracts/Ray.JdTool.Application.Contracts.csproj", "src/Ray.JdTool.Application.Contracts/"]
COPY ["src/Ray.JdTool.Domain.Shared/Ray.JdTool.Domain.Shared.csproj", "src/Ray.JdTool.Domain.Shared/"]
COPY ["src/Ray.JdTool.Domain/Ray.JdTool.Domain.csproj", "src/Ray.JdTool.Domain/"]
COPY ["src/Ray.JdTool.HttpApi/Ray.JdTool.HttpApi.csproj", "src/Ray.JdTool.HttpApi/"]
COPY ["src/Ray.JdTool.EntityFrameworkCore/Ray.JdTool.EntityFrameworkCore.csproj", "src/Ray.JdTool.EntityFrameworkCore/"]
COPY ["src/Ray.JdTool.Blazor/Ray.JdTool.Blazor.csproj", "src/Ray.JdTool.Blazor/"]
RUN dotnet restore "src/Ray.JdTool.HttpApi.Host/Ray.JdTool.HttpApi.Host.csproj"
RUN dotnet restore "src/Ray.JdTool.Blazor/Ray.JdTool.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Ray.JdTool.HttpApi.Host"
RUN dotnet build "Ray.JdTool.HttpApi.Host.csproj" -c Release -o /app/build

WORKDIR "/src/src/Ray.JdTool.Blazor"
RUN dotnet build "Ray.JdTool.Blazor.csproj" -c Release -o /app/blazorBuild

FROM build AS publish
WORKDIR "/src/src/Ray.JdTool.HttpApi.Host"
RUN dotnet publish "Ray.JdTool.HttpApi.Host.csproj" -c Release -o /app/publish
WORKDIR "/src/src/Ray.JdTool.Blazor"
RUN dotnet publish "Ray.JdTool.Blazor.csproj" -c Release -o /app/blazorPublish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /app/blazorPublish/wwwroot/* ./wwwroot
ENTRYPOINT ["dotnet","Ray.JdTool.HttpApi.Host.dll","--urls=http://0.0.0.0:9000"]