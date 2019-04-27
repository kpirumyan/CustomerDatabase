using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDatabase.Interfaces
{
  public interface IDataService
  {
    string ConnectionString { get; }
    NpgsqlDataAdapter Adapter { get; set; }

    void ConfigureAdapter(string tableName);
  }
}
