#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TRZ_WikimediaCount/TRZ_WikimediaCount.Command.csproj", "TRZ_WikimediaCount/"]
COPY ["TRZ_WikimediaCount.Infrastructure/TRZ_WikimediaCount.Infrastructure.csproj", "TRZ_WikimediaCount.Infrastructure/"]
COPY ["TRZ_WikimediaCount.Application/TRZ_WikimediaCount.Application.csproj", "TRZ_WikimediaCount.Application/"]
COPY ["TRZ_WikimediaCount.Core/TRZ_WikimediaCount.Core.csproj", "TRZ_WikimediaCount.Core/"]
RUN dotnet restore "TRZ_WikimediaCount/TRZ_WikimediaCount.Command.csproj"
COPY . .
WORKDIR "/src/TRZ_WikimediaCount"
RUN dotnet build "TRZ_WikimediaCount.Command.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TRZ_WikimediaCount.Command.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TRZ_WikimediaCount.Command.dll"]