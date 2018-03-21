Param(
    [string]$server,
    [string]$database,
    [string]$user,
    [string]$password
)

$uri = "postgresql://${user}:${password}@${server}/${database}"

& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_0_0_0.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_0.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_1.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_2.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_3.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_4.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_5.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_6.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_7.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_8.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_9.sql" $uri
& psql -f "$PSScriptRoot\..\src\BistroFiftyTwo.Database\schemapatch\dbc_1_0_10.sql" $uri
