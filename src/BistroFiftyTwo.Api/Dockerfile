FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT Development
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "BistroFiftyTwo.Api.dll"]
