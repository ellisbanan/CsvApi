# CsvApi

CsvApi is a simple REST API built with ASP.NET Core that reads data from CSV file and returns it in a JSON format.

## Features

- Read data from a CSV file
- Return data in JSON format via single endpoint: `GET /api/data`
- Supports an optional query parameter `limit` for limiting the number of records returned
- Easy to set up and run

## Cloning the Repository
To clone the repository, use the following command:
````sh
git clone https://github.com/ellisbanan/CsvApi
cd CsvApi
````

## How to Build
Go to the project directory and run the following command:
````sh
dotnet build
````

## How to Run
Go to the project directory and run the following command:
````sh
dotnet run
````

## How to Use
Once the application is running, you can access the API endpoint at: `http://localhost:xxxx/api/data`

Your specific port number will be displayed in the console when you run the application.

You can also limit the number of records returned by using the `limit` query parameter. For example, to get only 5 records, you can use the following URL: `http://localhost:xxxx/api/data?limit=5`

The project includes a `CsvApi.http` file that can be used for testing the API endpoint.

## CSV file example

By default, the API expects a CSV file named `data.csv` in the root directory of the project. 
If you want to specify a different file path, update the `CsvFilePath` value in `appsettings.json`.

The file should use the format: `ID;Name;Age;Email`

Example content of `data.csv`:
```
1;Alice;28;alice@example.com
2;Bea;12;bea@example.com
3;Ceasar;56;ceasar@example.com
4;David;3;david@example.com
5;Erik;67;erik@example.com
6;Fredrik;27;fredrik@example.com
7;Gustav;32;gustav@example.com
8;Henrik;43;henrik@example.com
9;Ivar;55;ivar@example.com
10;Johan;68;johan@example.com
11;Karl;72;karl@example.com
12;Lennart;89;lennart@example.com
13;Martin;26;martin@example.com
14;Niklas;17;niklas@example.com
15;Olov;40;olov@example.com
```

## Error Handling

- **500 Internal Server Error**: Returned if there is an issue reading the CSV file or processing the request.
- **400 Bad Request**: Returned if the `limit` query parameter is invalid (e.g., negative number).
- **204 No Content**: Returned if the CSV file is empty or contains no valid records.