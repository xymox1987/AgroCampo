#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AgroCampo_Back.sln", ""]
COPY ["AgroCampo_API/AgroCampo_API.csproj", "AgroCampo_API/"]
COPY ["AgroCampo_Business/AgroCampo_Business.csproj", "AgroCampo_Business/"]
COPY ["AgroCampo_DataAccess/AgroCampo_DataAccess.csproj", "AgroCampo_DataAccess/"]
COPY ["AgroCampo_Common/AgroCampo_Common.csproj", "AgroCampo_Common/"]
COPY ["AgroCampo_Domain/AgroCampo_Domain.csproj", "AgroCampo_Domain/"]


RUN dotnet restore "./AgroCampo_API/AgroCampo_API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AgroCampo_API/AgroCampo_API.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "AgroCampo_API/AgroCampo_API.csproj" -c Release -o /app/publish




FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apt-get update && apt-get install -y nodejs npm libfontconfig libgconf-2-4 wget
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub |  apt-key add - && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
RUN apt-get update && apt-get install -y google-chrome-unstable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst --no-install-recommends
RUN apt-get install -y libxtst6 libxss1

# RUN apt-get update && apt-get install -y  npm libfontconfig libgconf-2-4 wget

# RUN apt-get install -y   --no-install-recommends libgconf-2-4 gnupg git curl  ca-certificates libgconf-2-4 && \
#     wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - && \
#     sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' && \
#     apt-get update && \  
#     apt-get install -y lsb-release google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst libxtst6 libxss1 --no-install-recommends

ENV chrome_launchOptions_executablePath google-chrome-stable
ENV chrome_launchOptions_args --no-sandbox,--disable-dev-shm-usage,--single-process,--no-zygote



RUN cd /app/jsreport/ && rm -r data && npm install 


ENTRYPOINT ["dotnet", "AgroCampo_API.dll"]

# docker build -t sso_resource_esdav .
# docker run -d -p 8190:80 sso_resource_esdav
