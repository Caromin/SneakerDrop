FROM microsoft/dotnet:sdk
WORKDIR /app

COPY ./ /app
RUN dotnet build
RUN dotnet test
RUN dotnet publish -o ../Publish SneakerDrop.Mvc/SneakerDrop.Mvc.csproj

WORKDIR /app/Publish

EXPOSE 80

CMD ["dotnet", "SneakerDrop.dll"]