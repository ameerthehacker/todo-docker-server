FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY todo-server/todo-server.csproj todo-server/
RUN dotnet restore todo-server/todo-server.csproj
COPY . .
WORKDIR /src/todo-server
RUN dotnet build todo-server.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish todo-server.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "todo-server.dll"]
