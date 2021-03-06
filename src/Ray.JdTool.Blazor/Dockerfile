#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM nginx:alpine AS base
WORKDIR /app
EXPOSE 9000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Ray.JdTool.Blazor/Ray.JdTool.Blazor.csproj", "src/Ray.JdTool.Blazor/"]
COPY ["src/Ray.JdTool.HttpApi.Client/Ray.JdTool.HttpApi.Client.csproj", "src/Ray.JdTool.HttpApi.Client/"]
COPY ["src/Ray.JdTool.Application.Contracts/Ray.JdTool.Application.Contracts.csproj", "src/Ray.JdTool.Application.Contracts/"]
COPY ["src/Ray.JdTool.Domain.Shared/Ray.JdTool.Domain.Shared.csproj", "src/Ray.JdTool.Domain.Shared/"]
RUN dotnet restore "src/Ray.JdTool.Blazor/Ray.JdTool.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Ray.JdTool.Blazor"
RUN dotnet build "Ray.JdTool.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ray.JdTool.Blazor.csproj" -c Release --runtime linux-x64 --self-contained true -p:PublishTrimmed=true -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish/wwwroot .
COPY src/Ray.JdTool.Blazor/nginx.conf /etc/nginx/nginx.conf