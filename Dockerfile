FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5050

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src

COPY HolidayApi/HolidayApi.csproj /HolidayApi/
COPY Holiday/src/PublicHoliday/PublicHoliday.csproj /Holiday/ 
RUN dotnet restore /HolidayApi/HolidayApi.csproj
COPY . .
WORKDIR "/src/HolidayApi"
RUN dotnet build HolidayApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HolidayApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HolidayApi.dll"]







