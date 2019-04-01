FROM microsoft/dotnet:2.1-sdk
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore


COPY . ./

ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000   

RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/FindnChitChat.dll"]