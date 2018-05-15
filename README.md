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



``` 
dotnet run 
```


##### Questions/Commentary 

MSSql does not have an ARRAY datatype like some other languages and I found it more efficient to create a studyGuidesOrdered table. 

