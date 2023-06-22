using System.Data;
using Microsoft.Data.SqlClient;

namespace FluentApiExample;

public class FluentApiSqlConnection : IServerSelectionStage, IDatabaseSelectionStage, IUserSelectionStage,
    IPasswordSelectionStage, IConnectionInitializerStage
{
    private string _server;
    private string _database;
    private string _user;
    private string _password;

    private FluentApiSqlConnection()
    {
    }

    public static IServerSelectionStage CreateConnection(Action<ConnectionConfiguration> config)
    {
        var configuration = new ConnectionConfiguration();
        config?.Invoke(configuration); 
        return new FluentApiSqlConnection();
    }

    public IDatabaseSelectionStage ForServer(string server)
    {
        _server = server;
        return this;
    }

    public IUserSelectionStage ForDatabase(string database)
    {
        _database = database;
        return this;
    }

    public IPasswordSelectionStage ForUser(string user)
    {
        _user = user;
        return this;
    }

    public IConnectionInitializerStage WithPassword(string password)
    {
        _password = password;
        return this;
    }

    public IDbConnection Connect()
    {
        var connection =
            new SqlConnection($"Server={_server};Database={_database};User Id={_user};Password={_password}");
        connection.Open();
        return connection;
    }
}

public class ConnectionConfiguration
{
    public string ConnectionName { get; set; }
}

public interface IServerSelectionStage
{
    public IDatabaseSelectionStage ForServer(string server);
}

public interface IDatabaseSelectionStage
{
    public IUserSelectionStage ForDatabase(string database);
}

public interface IUserSelectionStage
{
    public IPasswordSelectionStage ForUser(string user);
}

public interface IPasswordSelectionStage
{
    public IConnectionInitializerStage WithPassword(string password);
}

public interface IConnectionInitializerStage
{
    public IDbConnection Connect();
}