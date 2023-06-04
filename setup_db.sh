#!/usr/bin/env bash
setup() {
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P LocalPassword123! -d master -i setup.sql >> ./db_setup.log 2>&1
}

startup() {
    echo 'Running DB setup...'
    until setup
    do
        echo 'Database not ready yet, retrying in 5 seconds...'
        sleep 5
    done
    echo 'DB setup complete!'
}

startup & /opt/mssql/bin/sqlservr > ./sql_server.log