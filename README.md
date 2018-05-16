# aspbasic

A basic dotnet core app for uploading files and adding info to the database. 

Created with dotnet core, Visual Studio OSX and the `microsoft/mssql-server-linux:2017-latest` docker image 



# Starting the app:

## Starting SQL Server Docker image:

## install it:

``` 
docker pull microsoft/mssql-server-linux:2017-latest
 ```

### Run it
``` 
    sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Passw0rd!' \
   -p 1433:1433 --name sql1 \
   -d microsoft/mssql-server-linux:2017-latest

```


### Running APP


#### Create the schema 

Add schema.sql to your database. If the connection string needs to change, please edit aspbasic.service.AspBasicService.connectionString



``` 
dotnet run 
```


##### Questions/TODO 

1. Ideally I would make a studyGuidesOrdered table to join the studyGuides and the Order. 
I ended up using a string but this makes it difficult to join the data effectively and would cause extra queries.
1. Unit Tests 
1. Better Exception Handling 
1. Externalizing configuration 
 



