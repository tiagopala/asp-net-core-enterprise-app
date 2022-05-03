sleep 35s

/opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U SA -P "MeuDB@123" -i db-dump-script.sql