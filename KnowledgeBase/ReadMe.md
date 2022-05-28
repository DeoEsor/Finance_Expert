
Docker Command

    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Pa55w0rd" -v mssql -p 1433:1433 -d --name mssqlserver -d mcr.microsoft.com/mssql/server:2019-latest