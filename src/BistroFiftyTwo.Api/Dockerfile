FROM microsoft/dotnet:2.1.4-aspnetcore-runtime
ARG source
WORKDIR /app
EXPOSE 80
RUN apt-get update \
    && apt-get install -y --no-install-recommends libunwind8 \
    && rm -rf /var/lib/apt/lists/*
ENV ASPNETCORE_ENVIRONMENT Development
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "BistroFiftyTwo.Api.dll"]
 