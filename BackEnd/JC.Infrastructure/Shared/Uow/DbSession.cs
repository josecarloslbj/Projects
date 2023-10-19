using MySql.Data.MySqlClient;
using System.Data;

namespace JC.Infrastructure.Shared.Uow;
public sealed class DbSession
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;

    public DbSession(string connectionString)
    {
        _connection = new MySqlConnection(connectionString);
    }

    public IDbConnection Connection => _connection;

    public IDbTransaction Transaction => _transaction;

    public IDbTransaction CreateTransaction()
    {
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();

        _transaction = _connection.BeginTransaction();

        return _transaction;
    }
}
