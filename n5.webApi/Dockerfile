#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["n5.webApi.csproj", "n5.webApi/"]
RUN dotnet restore "n5.webApi/n5.webApi.csproj"
COPY . /src/n5.webApi
WORKDIR "/src/n5.webApi"
RUN dotnet build "n5.webApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "n5.webApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "n5.webApi.dll"]