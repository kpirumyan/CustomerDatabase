using CustomerDatabase.Interfaces;
using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CustomerDatabase.Services
{
  public class DataService : IDataService
  {
    private readonly string _connectionString;

    public DataService(string tableName)
    {
      _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      ConfigureAdapter(tableName);
    }

    public NpgsqlDataAdapter Adapter { get; set; }

    public void ConfigureAdapter(string tableName)
    {
      Adapter = new NpgsqlDataAdapter($"Select * From {tableName}", _connectionString);
    }
  }
}
