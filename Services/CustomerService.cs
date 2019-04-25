using CustomerDatabase.Interfaces;
using CustomerDatabase.Models;
using Npgsql;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace CustomerDatabase.Services
{
  public class CustomerService //: ICustomerService
  {
    private readonly IDataService _dataService;

    public CustomerService()
    {
      _dataService = new DataService("Customers");
      Table = new DataTable("Customers");
    }

    public DataTable Table { get; set; }

    #region Methods

    //public void Insert(Customer customer)
    //{
    //  _dataService.Adapter.
    //}

    public void Update(Customer customer)
    {
      _dataService.Adapter.Fill(Table);
      var row = Table.NewRow();
      row["FirstName"] = customer.FirstName;
      row["LastName"] = customer.LastName;
      row["Age"] = customer.Age;
      row["Sex"] = customer.Sex;
      row["PassportId"] = customer.PassportId;
      row["Address"] = customer.Address;
      Table.Rows.Add(row);
      _dataService.Adapter.Update(Table);
    }

    public DataTable SelectAll()
    {
      _dataService.Adapter.Fill(Table);

      return Table;
    }

    public DataRowView FindByFullName(string str)
    {
      return null;
    }
    #endregion
  }
}
