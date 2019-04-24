using CustomerDatabase.Interfaces;
using CustomerDatabase.Models;
using Npgsql;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace CustomerDatabase.Services
{
  public class CustomerService : ICustomerService
  {
    private readonly IDataService _dataService;

    public CustomerService()
    {
      _dataService = new DataService("Customers");
    }

    #region Methods

    public async Task Insert(Customer customer)
    {

    }

    public async Task Update(Customer customer)
    {

    }

    public DataTable SelectAll()
    {
      DataTable table = new DataTable("Customers");
      _dataService.Adapter.Fill(table);

      return table;
    }

    public async Task<ObservableCollection<Customer>> FindByFullName(string str)
    {
      return null;
    }
    #endregion
  }
}
