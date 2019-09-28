FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Sqs.csproj", ""]
RUN dotnet restore "./Sqs.csproj"
COPY . .

RUN dotnet build "Sqs.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sqs.csproj" -c Release -o /app

FROM base AS final
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sqs.dll"]