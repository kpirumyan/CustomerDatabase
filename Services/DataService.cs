using CustomerDatabase.Interfaces;
using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CustomerDatabase.Services
{
  public class DataService : IDataService
  {
    //private string _connectionString;
    public string ConnectionString { get; }

    public DataService(string tableName)
    {
      ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      ConfigureAdapter(tableName);
    }

    public NpgsqlDataAdapter Adapter { get; set; }

    public void ConfigureAdapter(string tableName)
    {
      Adapter = new NpgsqlDataAdapter($"Select * From {tableName}", ConnectionString);
      if (!IsTableExists())
      {
        CreateTable(tableName);
      }

      var builder = new NpgsqlCommandBuilder(Adapter);
    }

    private void CreateTable(string tableName)
    {
      using (var conn = new NpgsqlConnection(ConnectionString))
      {
        conn.Open();
        var command = new NpgsqlCommand($@"CREATE TABLE public.{tableName}
          (
              ""Id"" serial PRIMARY KEY,
              ""FirstName"" varchar(60),
              ""LastName"" varchar(60),
              ""Age"" int,
              ""PassportId"" varchar(8),
              ""Sex"" varchar(20),
              ""Address"" varchar(120)
          );
          CREATE UNIQUE INDEX {tableName}_Id_uindex ON public.{tableName}(""Id"");
          CREATE UNIQUE INDEX {tableName}_PassportId_uindex ON public.{tableName}(""PassportId"");", conn);

        command.ExecuteNonQuery();
      }
    }

    private bool IsTableExists()
    {
      try
      {
        DataTable t = new DataTable();
        Adapter.Fill(t);
        return true;
      }
      catch (NpgsqlException)
      {
        return false;
      }
    }
  }
}
