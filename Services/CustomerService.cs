using CustomerDatabase.Interfaces;
using System.Data;

namespace CustomerDatabase.Services
{
  public class CustomerService : ICustomerService
  {
    private readonly IDataService _dataService;

    public CustomerService()
    {
      _dataService = new NpgsqlDataService("Customers");
      Table = new DataTable("Customers");
    }

    public DataTable Table { get; set; }

    #region Methods

    public void Insert(DataRow row)
    {
      _dataService.Adapter.Fill(Table);
      var newRow = Table.NewRow();
      newRow.ItemArray = row.ItemArray;
      Table.Rows.Add(newRow);
      _dataService.Adapter.Update(Table);
    }

    public void Update(DataRow row)
    {
      _dataService.Adapter.Fill(Table);
      var rowToUpdate = Table.Select($"Id = {row["Id"]}")[0];
      rowToUpdate.ItemArray = row.ItemArray;
      _dataService.Adapter.Update(Table);
    }

    public DataTable SelectAll()
    {
      Table.Clear();
      _dataService.Adapter.Fill(Table);

      return Table;
    }
    #endregion
  }
}
