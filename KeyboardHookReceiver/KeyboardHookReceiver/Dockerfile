FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KeyboardHookReceiver/KeyboardHookReceiver.csproj", "KeyboardHookReceiver/"]
RUN dotnet restore "KeyboardHookReceiver/KeyboardHookReceiver.csproj"
COPY . .
WORKDIR "/src/KeyboardHookReceiver"
RUN dotnet build "KeyboardHookReceiver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KeyboardHookReceiver.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeyboardHookReceiver.dll"]
