# ============================================================
# Build Stage
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# کپی کردن فایل‌های پروژه
COPY ["Accounting.Api/Accounting.Api.csproj", "Accounting.Api/"]
COPY ["Accounting.Domain/Accounting.Domain.csproj", "Accounting.Domain/"]
COPY ["Accounting.Infrastructure/Accounting.Infrastructure.csproj", "Accounting.Infrastructure/"]

# Restore
RUN dotnet restore "Accounting.Api/Accounting.Api.csproj"

# کپی کردن همه‌ی فایل‌ها
COPY . .

# Build
WORKDIR "/src/Accounting.Api"
RUN dotnet build "Accounting.Api.csproj" -c Release -o /app/build

# ============================================================
# Publish Stage
# ============================================================
FROM build AS publish
RUN dotnet publish "Accounting.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ============================================================
# Runtime Stage
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# کپی کردن خروجی publish
COPY --from=publish /app/publish .

# پورت
EXPOSE 8080

# Environment
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# اجرا
ENTRYPOINT ["dotnet", "Accounting.Api.dll"]