// See https://aka.ms/new-console-template for more information

using FluentApiExample;
using Microsoft.IdentityModel.Protocols;

var connection = FluentApiSqlConnection
    .CreateConnection(conf =>
    {
        conf.ConnectionName = "My connection"; // just for testing
    })
    .ForServer("server")
    .ForDatabase("database")
    .ForUser("user")
    .WithPassword("password")
    .Connect();


Console.WriteLine("Hello, World!");